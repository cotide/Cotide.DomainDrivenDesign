using Cotide.Domain.Enum;
using Cotide.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Commands.Client
{
    /// <summary>
    /// 创建客户端命令
    /// </summary>
    public class CreateClientCommand
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public readonly string UserName;

        /// <summary>
        /// 密码
        /// </summary>
        public readonly string Paw;

        /// <summary>
        /// 名字
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 客户端公钥
        /// </summary>
        public readonly string ClientSecret;

        /// <summary>
        /// 客户端识别号
        /// </summary>
        public readonly string ClientIdentifier;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <param name="clientSecret">客户端公钥</param>
        /// <param name="clientIdentifier">客户端唯一标识</param>
        public CreateClientCommand(
            string name,
            string userName,
            string paw,
            string clientSecret,
            string clientIdentifier)
        {
            Guard.IsNotNullOrEmpty(name,"name");
            Guard.IsNotNullOrEmpty(userName, "userName");
            Guard.IsNotNullOrEmpty(paw, "paw");
            Guard.IsNotNullOrEmpty(clientSecret, "clientSecret");
            Guard.IsNotNullOrEmpty(clientIdentifier, "clientIdentifier");
            Name = name;
            UserName = userName;
            Paw = paw;
            ClientSecret = clientSecret;
            ClientIdentifier = clientIdentifier;
            ClientState = ClientState.Pause;
        }

        /// <summary>
        /// 应用图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        ///  回调地址 (没有callback参数时候使用)
        /// </summary>
        public string RedirectUrl { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 客户端状态
        /// </summary>
        public ClientState ClientState { get; set; }
    }
}
