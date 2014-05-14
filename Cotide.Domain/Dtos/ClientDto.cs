using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos.Base;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Dtos
{
    /// <summary>
    /// 客户端DTO
    /// </summary>
    public class ClientDto : BaseDto<Guid>
    {
        /// <summary>
        /// 应用图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Paw { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户端公钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 客户端识别号
        /// </summary>
        public string ClientIdentifier { get; set; }
         
        /// <summary>
        /// 简介
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 客户端状态
        /// </summary>
        public ClientState ClientState { get; set; }

        /// <summary>
        /// 回调地址 (没有callback参数时候使用)
        /// </summary>
        public string RedirectUrl { get; set; }

         
    }
}
