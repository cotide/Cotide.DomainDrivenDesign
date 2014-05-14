using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos.Base;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Dtos
{
    public class ClientAuthorizationDto : BaseDto<Guid>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// 所属用户名
        /// </summary>
        public string  UserName { get; set; }

         
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string  Token { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public string  ClientName { get; set; }


        /// <summary>
        /// 鉴权类型
        /// </summary>
        public AuthType AuthType { get; set; }
    }
}
