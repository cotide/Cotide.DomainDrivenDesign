using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 令牌类型
    /// </summary>
    public enum AuthType
    {
        /// <summary>
        /// 鉴权
        /// </summary>
        [Description("鉴权")]
        Auth,
        /// <summary>
        /// Token
        /// </summary>
        [Description("Token")]
        Token
    }
}
