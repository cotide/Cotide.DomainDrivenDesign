using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Contracts.Commands.ClientAuthorization;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Entity;
using Cotide.Framework.Extensions;
using Cotide.Portal.Controllers.Controllers.Base;
using Cotide.Portal.Controllers.Core;
using Cotide.Portal.Controllers.ViewModels.OAuth;
using Cotide.Domain.Dtos;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Enum;

namespace Cotide.Portal.Controllers.Controllers
{
    /// <summary>
    /// 鉴权服务
    /// </summary>
    public class OAuthController : BaseController
    {

        protected IClientQueryService ClientQueryService;
        protected IClientTask ClientTask;
        protected IClientAuthorizationTask ClientAuthorizationTask;
        protected IClientAuthorizationQueryService ClientAuthorizationQueryService;
        protected IUserQueryService UserQueryService;
        protected IIdentityTask IdentityTask;

        /// <summary>
        /// 构造函数
        /// </summary>
        public OAuthController(
            IClientQueryService clientQueryService,
            IClientTask clientTask,
            IClientAuthorizationTask clientAuthorizationTask,
            IClientAuthorizationQueryService clientAuthorizationQueryService, 
            IUserQueryService userQueryService,
            IIdentityTask identityTask)
        {
            ClientQueryService = clientQueryService;
            ClientTask = clientTask;
            ClientAuthorizationTask = clientAuthorizationTask;
            ClientAuthorizationQueryService = clientAuthorizationQueryService;
            UserQueryService = userQueryService;
            IdentityTask = identityTask;
        }
 

        [HttpGet]
        public ActionResult Login(
            string clientId,
            string responseType,
            string scope = "snsapi_base",
            string state = "",
            string redirectUrl = "")
        {

            var client = ClientQueryService.Get(clientId); 
            return View("Login", new OauthLoginViewModel()
            {
                ClientId = client.ClientIdentifier,
                ClentName = client.Name,
                RedirectUrl = redirectUrl,
                ResponseType = responseType,
                Scope = scope,
                State = state,
                 UserViewModel = CurrentUser
            });
        }

        public ActionResult AutoLogin(
            string UserName,
            string Paw,
            string client_id,
            string response_type,
            string state = "",
            string redirect_uri = "",
            string scope = "snsapi_base")
        {
            var user = UserQueryService.FindOne(UserName, Paw);
            if (user != null)
            {
                IdentityTask.SignIn(user.Id, true, UserLoginRole.User);
                return Redirect(Url.Action("Authorize", new
                {
                    client_id =client_id,
                    response_type =response_type,
                    state =state,
                    redirect_uri=redirect_uri,
                    scope =scope
                })); 
            }
            TempData["alert"] = "<script>alert('账号密码不正确');</script>";
            return Login(client_id,response_type,scope,state,redirect_uri);
        }

        /// <summary>
        /// 授权申请
        /// </summary>
        /// <param name="client_id">客户端唯一标识</param>
        /// <param name="response_type">
        /// 请求类型   
        /// code : 代码方式  
        /// bearer : 直接检测 
        /// </param>
        /// <param name="state">状态码 (防CSRF 攻击)</param>
        /// <param name="redirect_uri">回调页面地址</param>
        /// <param name="scope">应用授权作用域 
        ///  snsapi_userinfo :（弹出授权页面,确定授权后，返回授权令牌）
        ///  </param>
        /// <returns></returns>  
        public ActionResult Authorize(
            string client_id,
            string response_type,
            string state = "",
            string redirect_uri = "",
            string scope = "snsapi_base"
            )
        {
            // 1. 根据Client ID 找到 Client（客户端）,检查客户端是否在合法使用时间内
            var client = ClientQueryService.Get(client_id);

            if (client == null)
            {
                return NotPower("No Found Client Info");
            }
             
            // 2. 确认Redirect URL 跟事先注册的是否相符
            // 2.1 若不正确则抛出错误,不可转回该redirect_uri
            string callBackUrl = GetRedirectUri(redirect_uri, client);
            if (callBackUrl == "")
            {
                return NotPower("Redirect Url Format Error");
            }

            // 3. 确认申请scope正确(格式,内容等)
             if (scope == "snsapi_base")
            {
                // snsapi_base :（不弹出授权页面，直接跳转，返回授权令牌）
                if (CurrentUser != null)
                {
                    int timeOut = 0;
                    int.TryParse(ConfigurationManager.AppSettings["TokenTimeOut"], out timeOut);

                    // 4. 没问题就请求Resource Owner要不要授权 
                    var grantCode =
                        ClientAuthorizationTask.Create(new CreateTokenCommand(
                            client_id,
                            Domain.Enum.AuthType.Auth)
                        {
                            TimeOut = timeOut,
                             UserId =CurrentUser.UserId
                        });

                    var outPutUrl = string.Format("{0}{1}code={2}&state={3}&timeOut={4}",
                        callBackUrl,
                        callBackUrl.IsHaveUrlParameter() ? "&" : "?",
                        grantCode.Token,
                        state,
                        grantCode.ExpirationTime);

                    return Redirect(outPutUrl);
                }
                else
                {
                    // snsapi_userinfo :（弹出授权页面,确定授权后，返回授权令牌）
                    return Login(
                        client.ClientIdentifier,
                        response_type,
                        "snsapi_base",
                        state,
                        redirect_uri ?? client.RedirectUrl);

                }
            } 
            if (scope == "snsapi_userinfo")
            {

                // snsapi_userinfo :（弹出授权页面,确定授权后，返回授权令牌）
                return Login(
                    client.ClientIdentifier,
                    response_type,
                    "snsapi_base",
                    state,
                    redirect_uri ?? client.RedirectUrl);

            }
            // snsapi_userinfo :（弹出授权页面,确定授权后，返回授权令牌）
            return Login(
                client.ClientIdentifier,
                response_type,
                "snsapi_base",
                state,
                redirect_uri ?? client.RedirectUrl); 
        
        }

        /// <summary>
        /// 获取访问令牌
        /// </summary> 
        /// <param name="grant_type">授权状态
        /// 请求方式 
        /// auth: 自动验证用户身份
        /// userAuth: 验证User(ID/Paw)
        /// clientAuth: 验证Client(ID/Paw)
        /// refresh_token : 刷新令牌
        /// </param>
        /// <param name="code">
        /// 授权码
        /// </param>
        /// <param name="state">状态码 (防CSRF 攻击)</param>
        /// <param name="redirect_uri">回调页面地址</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Token(
            string grant_type,
            string code,
            string state,
            string redirect_uri = ""
           )
        {

            // 验证参数
            // 1. 确认Client Authentication(ID/Secret)正确 
            if (Request.Headers["Authorization"] == null)
            {
                ClientAuthorizationTask.Delete(new DeleteTokenCommand(code)); 
                return NotPower("Client Authentication Error:未知请求头");
            }
            var array = Request.Headers["Authorization"].Split(':');
            if (array.Length != 2)
            {
                ClientAuthorizationTask.Delete(new DeleteTokenCommand(code)); 
                return NotPower("Client Authentication Error:格式不正确");
            }

            // 检查验证方式
            switch (grant_type)
            {
                // 直接检查账号密码
                case "clientAuth":
                    var client = ClientQueryService.GetClient(array[0], array[1]);
                    if (client == null)
                    {

                        ClientAuthorizationTask.Delete(new DeleteTokenCommand(code)); 
                        return NotPower("Client Authentication Error:非法用户");
                    }
                     

                    // 2. 找到该Grant Code,确认其Redirect URL 跟事先注册的是否相符
                    if (ClientAuthorizationQueryService.CheckAuthToken(client.ClientIdentifier, code, AuthType.Auth) == false)
                    {

                        ClientAuthorizationTask.Delete(new DeleteTokenCommand(code)); 
                        return NotPower("Client Authentication Error:错误的授权码");
                    }
                     
                    string callBackUrl = GetRedirectUri(redirect_uri, client);
                    if (callBackUrl == "")
                    {

                        ClientAuthorizationTask.Delete(new DeleteTokenCommand(code)); 
                        return NotPower("Redirect Url Format Error");
                    }
                    // 3. 没问题就发放Token    
                    int timeOut = 0;
                    int.TryParse(ConfigurationManager.AppSettings["TokenTimeOut"], out timeOut);
                    var dto = ClientAuthorizationQueryService.Get(code);
                    var result = ClientAuthorizationTask.Create(new CreateTokenCommand(client.ClientIdentifier, AuthType.Token)
                    {
                        UserId = dto.UserId, 
                        TimeOut = timeOut
                    }); 
                    // 删除授权令牌
                    ClientAuthorizationTask.Delete(new DeleteTokenCommand(code));  
                    return Json(new TokenInfo()
                    { 
                         AccessToken = result.Token,
                         ExpirationTime = result.ExpirationTime,
                         TokenType = "Token" 
                    });
            }


            // 发放Token
            // 条件：
            // 1. 返回JSON格式
            // 2. 可以一并发 Refresh Token
            // 3. User 授权的Scope 若与申请时不一致则要附上
            // 响应实体对象：
            // WebOutputResult<>

            return NotPower("暂不之前其他授权类型");
        }



        #region Helper

        /// <summary>
        /// 检查回调URL是否正确
        /// </summary>
        private string GetRedirectUri(string callbackUrl, ClientDto client)
        {
            if (string.IsNullOrEmpty(callbackUrl))
            {
                if (string.IsNullOrEmpty(client.RedirectUrl))
                {
                    return string.Empty;
                }
                return client.RedirectUrl;


            }
            else
            {
                // 校验callbackUrl 和 client.RedirectUrl 的域名是否一致.
                if (callbackUrl.GetDomain().Trim() != client.RedirectUrl.GetDomain().Trim())
                {
                    return string.Empty;
                }
                return callbackUrl;
            }
        }
        #endregion
    }
}
