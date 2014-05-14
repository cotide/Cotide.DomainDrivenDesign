using System;

namespace Cotide.Portal.Controllers.Core
{
    /// <summary>
    /// Token信息
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Token类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public DateTime ExpirationTime { get; set; }


        /// <summary>
        /// 应用授权作用域
        /// </summary>
        public string Scopes { get; set; }
    }
}