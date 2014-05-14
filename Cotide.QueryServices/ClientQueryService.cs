using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Framework.Mapper;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 客户端查询
    /// </summary>
    public class ClientQueryService : IClientQueryService
    {
        public IClientRepository ClientRepository;

        public ClientQueryService(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public ClientDto Get(string clientId)
        {
            return ClientRepository.FindAll().FirstOrDefault(x => x.ClientIdentifier == clientId).MapTo<ClientDto>();
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientDto Get(Guid id)
        {
            return ClientRepository.FindAll().FirstOrDefault(x => x.Id == id).MapTo<ClientDto>();
        }

        /// <summary>
        /// 获取所有客户端
        /// </summary>
        /// <returns></returns>
        public IList<ClientDto> FindAll()
        {
            return ClientRepository.FindAll().Select(x => new ClientDto()
            {
                ClientIdentifier = x.ClientIdentifier,
                ClientSecret = x.ClientSecret,
                CreateDateTime = x.CreateDateTime,
                Id = x.Id,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                Paw = x.Paw,
                RedirectUrl = x.RedirectUrl,
                UserName = x.UserName,
                Desc = x.Desc,
                ClientState = x.ClientState,
                Img = x.Img
            }).ToList();
        }

        public ClientDto Get(string userName, string paw)
        {
            return ClientRepository.FindAll().FirstOrDefault(x => x.UserName == userName
                                                                  && x.Paw == paw).MapTo<ClientDto>();
        }

        public ClientDto GetClient(string clientIdentifier, string clientSecret)
        {
            return ClientRepository.FindAll().FirstOrDefault(x => x.ClientIdentifier == clientIdentifier
                                                                     && x.ClientSecret == clientSecret).MapTo<ClientDto>();
        }
    }
}
