using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos.Base;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Dtos
{
    /// <summary>
    /// 用户Dt0
    /// </summary>
    public class UserDto : BaseDto<Guid>
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Paw { get; set; }

    }
}
