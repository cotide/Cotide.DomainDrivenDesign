using Cotide.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Commands.User
{
    public class CreateUserCommand
    {
        public readonly string UserName;

        public readonly string RealName;

        public readonly string Paw;

        public CreateUserCommand(string realName, string userName, string paw)
        {
            Guard.IsNotNullOrEmpty(realName, "realName");
            Guard.IsNotNullOrEmpty(userName, "userName");
            Guard.IsNotNullOrEmpty(userName, "paw");
            RealName = realName;
            UserName = userName;
            Paw = paw;
        }
    }
}
