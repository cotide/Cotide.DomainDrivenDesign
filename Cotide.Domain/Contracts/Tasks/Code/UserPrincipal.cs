using System;
using System.Security.Principal;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.Tasks.Code
{
    public class UserPrincipal : IPrincipal
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        readonly IIdentity _owrIdentity;

        /// <summary>
        /// 当前用户
        /// </summary>
        readonly IdentityUser _user;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="user"></param> 
        public UserPrincipal(
            IIdentity identity,
            IdentityUser user)
        {
            _owrIdentity = identity;
            _user = user;
        }


        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity
        {
            get { return _owrIdentity; }
        }

        #region 属性

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserLoginRole UserRole
        {
            get
            {
                return _user.UserLoginRole;
            }
        }

        

        /// <summary>
        /// ID
        /// </summary>
        public Guid UserId { get { return _user.ID; } }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get { return _user.UserName; } }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get { return _user.RealName; } }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _user.CreateDate; }
        }

        #endregion
    }
}
