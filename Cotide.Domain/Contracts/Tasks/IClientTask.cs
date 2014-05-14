using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Dtos;

namespace Cotide.Domain.Contracts.Tasks
{
    public interface IClientTask
    {
        /// <summary>
        /// 创建客户端
        /// </summary>
        /// <param name="command"></param>
        void Create(CreateClientCommand command);

        /// <summary>
        /// 更新客户端
        /// </summary>
        /// <param name="command"></param>
        void Update(UpdateClientCommand command);

        /// <summary>
        /// 删除客户端
        /// </summary>
        /// <param name="command"></param>
        void Delete(DeleteClientCommand command);

    }
}
