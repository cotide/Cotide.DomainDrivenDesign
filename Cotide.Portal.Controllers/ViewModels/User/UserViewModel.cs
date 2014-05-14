using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Enum;

namespace Cotide.Portal.Controllers.ViewModels.User
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
         
        /// <summary>
        /// 用户角色
        /// </summary>
        public UserLoginRole UserRole { get; set; }
    }
}
