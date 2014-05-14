using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Domain.Enum;
using Cotide.Framework.Utility;

namespace Cotide.Tasks
{
    public class ClientTask : IClientTask
    {
        protected IClientRepository ClientRepository;

        protected IClientAuthorizationRepository ClientAuthorizationRepository;


        public ClientTask(
            IClientRepository clientRepository,
            IClientAuthorizationRepository clientAuthorizationRepository)
        {
            ClientRepository = clientRepository;
            ClientAuthorizationRepository = clientAuthorizationRepository;
        }

        public void Create(CreateClientCommand command)
        {
            ClientRepository.Create(new Domain.Entity.Client()
            {
                ClientIdentifier = command.ClientIdentifier,
                ClientSecret = command.ClientSecret,
                LastUpdateDateTime = DateTime.Now,
                CreateDateTime = DateTime.Now,
                Name = command.Name,
                Paw = command.Paw,
                RedirectUrl = command.RedirectUrl,
                UserName = command.UserName,
                ClientState = command.ClientState,
                Desc = command.Desc,
                Img = command.Img
            });
        }

        public void Update(UpdateClientCommand command)
        {
            var client = ClientRepository.FindAll().FirstOrDefault(x => x.Id == command.Id);
            Guard.IsNotNull(client, "client");
            client.UserName = command.UserName;
            client.Paw = command.Paw;
            client.Name = command.Name;
            client.ClientSecret = command.ClientSecret;
            client.ClientIdentifier = command.ClientIdentifier;
            client.RedirectUrl = command.RedirectUrl;
            client.Img = command.Img;
            client.Desc = command.Desc;
            if (command.ClientState != null)
            { 
               client.ClientState = (ClientState)command.ClientState;
            } 
            ClientRepository.Update(client);
        }

        public void Delete(DeleteClientCommand command)
        {
            var client = ClientRepository.FindAll().FirstOrDefault(x => x.Id == command.Id);
            Guard.IsNotNull(client, "client");
            ClientRepository.Delete(client);
        }

    }
}
