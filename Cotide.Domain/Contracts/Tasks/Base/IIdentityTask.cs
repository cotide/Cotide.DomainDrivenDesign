using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Cotide.Domain.Contracts.Tasks.Code;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.Tasks
{
    /// <summary>
    /// 用户状态管理服务接口
    /// </summary>
    public interface IIdentityTask
    {
        /// <summary>
        /// 用户登录地址
        /// </summary>
        string UserLogin { get; }

        /// <summary>
        /// 后台用户登录地址
        /// </summary>
        string ClientLogin { get; }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId">用户ID</param> 
        /// <param name="createPersistentCookies">是否跨游览器保存凭据</param>
        /// <param name="userRole">用户角色</param>
        void SignIn(
            Guid userId,
            bool createPersistentCookies,
            UserLoginRole userRole = UserLoginRole.User);
         

        /// <summary>
        /// 注销
        /// </summary> 
        void SignOut();
         
        /// <summary>
        /// 是否已经登录
        /// </summary> 
        /// <returns></returns>
        bool IsSignedIn();

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        IIdentity GetCurrentIdentity();

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        UserPrincipal GetCurrentUser();

    }
}
