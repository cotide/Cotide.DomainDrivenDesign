using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;

namespace Cotide.QueryServices
{
    public class ClientAuthorizationQueryService : IClientAuthorizationQueryService
    {
        protected IClientAuthorizationRepository ClientAuthorizationRepository;

        public ClientAuthorizationQueryService(IClientAuthorizationRepository clientAuthorizationRepository)
        {
            ClientAuthorizationRepository = clientAuthorizationRepository;
        }


        public bool CheckAuthToken(string clientId, string code, AuthType authType)
        {
            var query = (from d in ClientAuthorizationRepository.FindAll()
                         let u = d.User
                         let c = d.Client
                         where c.ClientIdentifier == clientId
                         && d.Token == code
                         select d).FirstOrDefault();
            if (query != null)
                return true;
            return false;
        }

        public ClientAuthorizationDto Get(string token)
        {
            return (from d in ClientAuthorizationRepository.FindAll()
                    let u = d.User
                    let c = d.Client
                    where d.Token == token
                    && DateTime.Now <= d.ExpirationTime
                    select new ClientAuthorizationDto()
                    {
                        AuthType = d.AuthType,
                        ClientName = d.Client.Name,
                        CreateDateTime = d.CreateDateTime,
                        CreateTime = d.CreateTime,
                        ExpirationTime = d.ExpirationTime,
                        Id = d.Id,
                        LastUpdateDateTime = d.LastUpdateDateTime,
                        Token = d.Token,
                        UserId = d.User.Id,
                        UserName = d.User.UserName
                    }).FirstOrDefault();
        }
    }
}
