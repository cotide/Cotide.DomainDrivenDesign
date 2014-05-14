using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Contracts.Tasks.Code;
using Cotide.Domain.Enum;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;

namespace Cotide.Tasks
{
    /// <summary>
    /// 用户状态管理服务
    /// </summary>
    public sealed class IdentityTask : IIdentityTask
    {
        private readonly IUserInfoRepository _userRepository;  
        
        /// <summary>
        /// 用户凭证加密密钥
        /// </summary>
        private readonly string _secret = ConfigurationManager.AppSettings["UserSecretKey"];
        private readonly string _userLogin = ConfigurationManager.AppSettings["UserLogin"];
        private readonly string _adminLogin = ConfigurationManager.AppSettings["ClientLogin"];
         
        /// <summary>
        /// 用户登录地址
        /// </summary>
        public string UserLogin
        {
            get { return _userLogin; }
        }

        /// <summary>
        /// 后台用户登录地址
        /// </summary>
        public string ClientLogin
        {
            get { return _adminLogin; }
        }

         
    
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="userRepository"></param> 
        public IdentityTask(
            IUserInfoRepository userRepository )
        {
            _userRepository = userRepository; 
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId">用户ID</param> 
        /// <param name="createPersistentCookie">是否跨游览器保存凭据</param>
        /// <param name="userRole">用户角色</param>
        public void SignIn(
            Guid userId,
            bool createPersistentCookie, 
            UserLoginRole userRole = UserLoginRole.User)
        {
            var ticket = CreateTicket(userId.ToString(), userRole);
            FormsAuthentication.SetAuthCookie(ticket, createPersistentCookie);
        }

      
        /// <summary>
        /// 注销
        /// </summary> 
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 是否已经登录
        /// </summary> 
        /// <returns></returns>
        public bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public IIdentity GetCurrentIdentity()
        {
            return HttpContext.Current.User.Identity;
        }
         
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public UserPrincipal GetCurrentUser()
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userInfo = GetCurrentIdentityUser();
                return new UserPrincipal(
                    HttpContext.Current.User.Identity, userInfo);
            }
            return null;
        }


        #region Helper

        /// <summary>
        /// 生成包含用户角色的验证票据 (加密处理)
        /// </summary>
        /// <param name="key">用户ID</param>
        /// <param name="userRole">用户角色</param> 
        /// <returns></returns>
        private string CreateTicket(
            string key,
            UserLoginRole userRole)
        {
            var ticket = key + "|" + userRole;
            return CryptTools.Encrypt(ticket, _secret); 
        }
 
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private IdentityUser GetCurrentIdentityUser()
        {
            // 获取验证票
            var ticket = HttpContext.Current.User.Identity.Name;
            var decryptTicket = "";
            try
            {
                // 解密后的验证票
                decryptTicket = CryptTools.Decrypt(ticket, _secret);
            }
            catch (ArgumentException ex)
            { 
                SignOut();
                throw new UserPowerException("无效的用户凭证");
            }

            var userContent = decryptTicket.Split('|');
            if (!userContent.Any() || userContent.Count() != 2)
            { 
                SignOut();
                throw new UserPowerException("无效的用户凭证");
            } 

            var userRole = (UserLoginRole)Enum.Parse(typeof(UserLoginRole), userContent[1]);


            switch (userRole)
            {
                    // 管理员用户
                    case UserLoginRole.Admin: 
                    var adminId = Guid.Parse(userContent[0]); 
                    var admin = _userRepository.FindAll().FirstOrDefault(x => x.Id == adminId);
                    Guard.IsNotNull(admin, "user");
                    return new IdentityUser()
                        {
                            CreateDate = admin.CreateDateTime,
                            ID = admin.Id,
                            RealName = admin.RealName,
                            UserLoginRole = UserLoginRole.Admin,
                            UserName = admin.UserName
                        };


                    break;
                    // 供应商用户
                    case UserLoginRole.Supplier:
                  /*  var supplierId = Guid.Parse(userContent[0]);
                    var supplier = _supplierRepository.FindAll().FirstOrDefault(x => x.Id == supplierId);
                    Guard.IsNotNull(supplier, "supplier");
                    return new IdentityUser()
                        {
                            CreateDate = supplier.CreateDateTime,
                            ID = supplier.Id,
                            RealName = supplier.RealName,
                            UserLoginRole = UserLoginRole.Admin,
                            UserName = supplier.UserName
                        };  */
                     break;
                    // 普通用户
                    case UserLoginRole.User:
                     var userId = Guid.Parse(userContent[0]);
                     var user = _userRepository.FindAll().FirstOrDefault(x => x.Id == userId);
                    Guard.IsNotNull(user,"user");
                    return new IdentityUser()
                        {
                            CreateDate = user.CreateDateTime,
                            ID = user.Id,
                            RealName = user.RealName,
                            UserLoginRole =  UserLoginRole.User,
                            UserName = user.UserName
                        }; 
                    break;
                  
            } 
            SignOut();
            throw new UserPowerException("无效的用户凭证");
        }

        #endregion
    }
}
