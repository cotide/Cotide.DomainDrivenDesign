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
using Cotide.Framework.Extensions;
using Cotide.Framework.Utility;
using Cotide.Infrastructure.Repositories.Base;

namespace Cotide.Tasks
{
    public class ClientTask : DefaultRepositoryBase ,IClientTask
    {
       

        public ClientTask( )
        { 
        }

        public void Create(CreateClientCommand command)
        {

            using (var db = base.NewDb())
            {
                db.Client.Add(new Domain.Entity.Client()
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
                db.SaveChanges();
            }
        }

        public void Update(UpdateClientCommand command)
        {
            using (var db = base.NewDb())
            { 
                var client = db.FindAll<Client,Guid>().FirstOrDefault(x => x.Id == command.Id);
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
                    client.ClientState = (ClientState) command.ClientState;
                } 
                db.SaveChanges();
            }
        }


        public void Delete(DeleteClientCommand command)
        {
            using (var db = base.NewDb())
            {
                var client = db.FindAll<Client,Guid>().FirstOrDefault(x => x.Id == command.Id);
                Guard.IsNotNull(client, "client");
                db.Client.Remove(client);
                db.SaveChanges();
            }
        }

    }
}
