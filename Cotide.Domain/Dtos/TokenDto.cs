using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Dtos
{
    /// <summary>
    /// Token信息
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// 令牌类型
        /// </summary>
        public AuthType AuthType { get; set; }
    }
}
