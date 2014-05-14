using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 客户端查询接口
    /// </summary>
    public interface IClientQueryService
    {
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <returns></returns>
        ClientDto Get(string clientId);

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ClientDto Get(Guid id);

        /// <summary>
        /// 获取所有客户端
        /// </summary>
        /// <returns></returns>
        IList<ClientDto> FindAll();

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <returns></returns>
        ClientDto Get(string userName, string paw);


        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="clientIdentifier">客户端唯一标示</param>
        /// <param name="clientSecret">客户端公钥</param>
        /// <returns></returns>
        ClientDto GetClient(string clientIdentifier, string clientSecret);
    }
}
