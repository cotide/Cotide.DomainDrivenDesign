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
using Cotide.Framework.Extensions;
using Cotide.Infrastructure.Repositories;
using Cotide.Framework.Utility;
using Cotide.Infrastructure.Repositories.Base;

namespace Cotide.Tasks
{
    public class ClientAuthorizationTask : DefaultRepositoryBase ,IClientAuthorizationTask
    {

      

        public ClientAuthorizationTask()
        {
             
        }


        public TokenDto Create(CreateTokenCommand command)
        {
            using (var db = base.NewDb())
            {
                var client = db.Client.FirstOrDefault(x => x.ClientIdentifier == command.ClientId);

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
                    var user = db.FindOne<UserInfo, Guid>(x => x.Id == command.UserId);
                    result.User = user;
                }

                db.ClientAuthorization.Add(result);
                db.SaveChanges();

                return new TokenDto()
                {
                    AuthType = command.AuthType,
                    CreateTime = result.CreateDateTime,
                    ExpirationTime = result.ExpirationTime,
                    Id = result.Id,
                    Token = result.Token 
                };
            }
        }

        public void Delete(DeleteTokenCommand command)
        {
            var token = base.NewDb().FindAll<ClientAuthorization,Guid>().FirstOrDefault(x => x.Token == command.Code);
            if (token != null)
            {
                using (var db = base.NewDb())
                { 
                    db.ClientAuthorization.Remove(token); 
                    db.SaveChanges();
                }
            }
        }
    }
}
