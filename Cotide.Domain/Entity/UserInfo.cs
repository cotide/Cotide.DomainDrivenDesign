using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity.Base;

namespace Cotide.Domain.Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserInfo : EntityWidthGuidType
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Paw { get; set; }
    }
}
