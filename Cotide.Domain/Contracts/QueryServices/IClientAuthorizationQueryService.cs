using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.QueryServices
{
    public interface IClientAuthorizationQueryService
    {
        /// <summary>
        /// 判断授权是否存在
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="code"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        bool CheckAuthToken(string clientId, string code, AuthType authType);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClientAuthorizationDto Get(string token);
    }
}
