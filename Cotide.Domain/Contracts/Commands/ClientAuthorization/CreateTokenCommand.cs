using Cotide.Domain.Enum;
using Cotide.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Commands.Client
{
    public class CreateTokenCommand
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public  readonly string ClientId;

        /// <summary>
        /// 令牌类型
        /// </summary>
        public readonly AuthType AuthType;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CreateTokenCommand(string clientId, AuthType authType)
        {
            Guard.IsNotNullOrEmpty(clientId, "clientId");
            this.ClientId = clientId;
            AuthType = authType;
            TimeOut = 3600;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 默认为3600秒
        /// </summary>
        public int TimeOut;
    }
}
