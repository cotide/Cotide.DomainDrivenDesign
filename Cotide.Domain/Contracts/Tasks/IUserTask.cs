using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.User;

namespace Cotide.Domain.Contracts.Tasks
{
    public interface IUserTask
    {
          void Create(CreateUserCommand command);
    }
}
