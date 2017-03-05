using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.User;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Infrastructure.Repositories.Base;

namespace Cotide.Tasks
{
    public class UserTask : DefaultRepositoryBase,  IUserTask
    {
       

        public UserTask()
        {
         
        }

        public void Create(CreateUserCommand command)
        {
            using (var db = base.NewDb())
            {
                db.UserInfo.Add(new Domain.Entity.UserInfo()
                {
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    Paw = command.Paw,
                    RealName = command.RealName,
                    UserName = command.UserName
                });
                db.SaveChanges();
            }
        }
    }
}
