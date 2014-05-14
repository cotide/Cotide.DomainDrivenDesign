using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Contracts.Commands.ClientAuthorization;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Infrastructure.Repositories;
using Cotide.Framework.Utility;

namespace Cotide.Tasks
{
    public class ClientAuthorizationTask : IClientAuthorizationTask
    {

        protected IClientRepository ClientRepository;
        protected IClientAuthorizationRepository ClientAuthorizationRepository;
        protected IUserInfoRepository UserInfoRepository;

        public ClientAuthorizationTask(
            IClientRepository clientRepository,
            IClientAuthorizationRepository clientAuthorizationRepository,
            IUserInfoRepository userInfoRepository)
        {
            ClientRepository = clientRepository;
            ClientAuthorizationRepository = clientAuthorizationRepository;
            UserInfoRepository = userInfoRepository;
        }


        public TokenDto Create(CreateTokenCommand command)
        {
            var client = ClientRepository.FindAll().FirstOrDefault(x => x.ClientIdentifier == command.ClientId);

           
            var result = new ClientAuthorization()
            {
                Client = client,
                CreateTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddSeconds(command.TimeOut),
                Token = Guid.NewGuid().ToString("N"),
                AuthType = command.AuthType,
                LastUpdateDateTime = DateTime.Now,
                CreateDateTime = DateTime.Now
            };
            if (command.UserId != null)
            {
                var user = UserInfoRepository.Get((Guid)command.UserId);
                result.User = user;
            }

            var id = ClientAuthorizationRepository.Create(result);

            return new TokenDto()
            {
                AuthType = command.AuthType,
                CreateTime = result.CreateDateTime,
                ExpirationTime = result.ExpirationTime,
                Id = result.Id,
                Token = result.Token

            };
        }

        public void Delete(DeleteTokenCommand command)
        {
            var token = ClientAuthorizationRepository.FindAll().FirstOrDefault(x => x.Token == command.Code);
            if (token != null)
            {
                ClientAuthorizationRepository.Delete(token);
            }
        }
    }
}
