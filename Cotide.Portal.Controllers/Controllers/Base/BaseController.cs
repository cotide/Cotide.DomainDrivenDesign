using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Cotide.Domain.Contracts.Tasks.Code;
using Cotide.Domain.Dtos;
using Cotide.Framework.Utility;
using Cotide.Portal.Controllers.Core;
using Cotide.Portal.Controllers.ViewModels.User;

namespace Cotide.Portal.Controllers.Controllers.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : Controller
    {


        /// <summary>
        /// 是否显示OAth鉴权的错误信息
        /// </summary>
        protected bool IsShowOAuthError = false;


        /// <summary>
        /// Token超时时间
        /// </summary>
        protected long TokenTimeOut = -1;


        public BaseController()
        {
            if (ConfigurationManager.AppSettings["IsShowOAuthError"] != null)
            {
                bool.TryParse(ConfigurationManager.AppSettings["IsShowOAuthError"], out IsShowOAuthError);
            }
            if (ConfigurationManager.AppSettings["TokenTimeOut"] != null)
            {
                long.TryParse(ConfigurationManager.AppSettings["TokenTimeOut"], out TokenTimeOut);
            }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected virtual UserViewModel CurrentUser
        {
            get
            {
                var user = HttpContext.User as UserPrincipal;
                return user == null ? null : CreateUserViewModel(user);
            }
        }

        /// <summary>
        /// 判断当前用户是否已登录
        /// </summary>
        protected virtual bool IsLogin
        {
            get { return CurrentUser != null; }

        }

        /// <summary>
        /// 输出Alter
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual void Alter(string msg)
        {
            var str = new StringBuilder("<script type=\"text/javascript\">");
            str.AppendFormat("alert('{0}');",msg);
            str.Append("</script>");
            TempData["alter"] = new MvcHtmlString(str.ToString());
        }

        /// <summary>
        /// 没有权限视图
        /// </summary>
        /// <returns></returns>
        protected ActionResult NotPower(string msg = "")
        {
            if (IsShowOAuthError == true)
            {
                var result = new WebOutputResult<ErrorMsg>();
                result.OutPutDateTime = DateTime.Now;
                result.Data = new ErrorMsg()
                {
                    ErrorCode = "401",
                    ErrorDesc = msg ?? "非法操作",
                    ErrorUrl = HttpContext.Request.Url.AbsolutePath,

                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                this.HttpContext.Response.Clear();
                this.HttpContext.Response.StatusCode = 401;
                this.HttpContext.Response.End();
                return new EmptyResult();
            }
        }

        #region Helper

        private UserViewModel CreateUserViewModel(UserPrincipal userPrincipal)
        {
            return new UserViewModel()
                {
                    RealName = userPrincipal.RealName,
                    UserId = userPrincipal.UserId,
                    UserName = userPrincipal.UserName,
                    UserRole = userPrincipal.UserRole
                };
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns>图片</returns>
        [AllowAnonymous]
        public ActionResult GetCaptcha()
        {
            var captcha = new Captcha();
            var code = captcha.CreateValidateCode(5);
            Session["captcha"] = code;
            var bytes = captcha.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        #endregion
    }
}
