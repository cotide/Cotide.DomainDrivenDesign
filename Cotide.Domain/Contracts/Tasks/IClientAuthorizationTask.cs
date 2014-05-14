using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Contracts.Commands.ClientAuthorization;
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.Tasks
{
    public interface IClientAuthorizationTask
    {
        /// <summary>
        /// 创建客户端鉴权信息/Token
        /// </summary>
        /// <param name="command"></param>
        TokenDto Create(CreateTokenCommand command);

        /// <summary>
        /// 删除客户端鉴权信息
        /// </summary>
        /// <param name="command"></param>
        void Delete(DeleteTokenCommand command);

    }
}
