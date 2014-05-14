using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity.Base;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Entity
{
    /// <summary>
    /// 客户端鉴权信息
    /// </summary>
    public class ClientAuthorization : EntityWidthGuidType
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
        /// 所属用户
        /// </summary>
        public UserInfo User { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public Client Client { get; set; }


        /// <summary>
        /// 鉴权类型
        /// </summary>
        public AuthType AuthType { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int TimeOut
        {
            get { return (ExpirationTime - CreateTime).Seconds; }
        }
    }
}
