using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 用户登录角色
    /// </summary>
    [Flags]
    public enum UserLoginRole
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User = 0,
         
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin = 2,

        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
        Supplier=3
    }
}
