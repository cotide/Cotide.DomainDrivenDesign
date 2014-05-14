using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.User;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Tasks;

namespace Cotide.Tasks
{
    public class UserTask : IUserTask
    {
        protected IUserInfoRepository UserInfoRepository;

        public UserTask(IUserInfoRepository userInfoRepository)
        {
            UserInfoRepository = userInfoRepository;
        }

        public void Create(CreateUserCommand command)
        {
            UserInfoRepository.Create(new Domain.Entity.UserInfo()
            {
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                Paw = command.Paw,
                RealName = command.RealName,
                UserName = command.UserName
            });
        }
    }
}
