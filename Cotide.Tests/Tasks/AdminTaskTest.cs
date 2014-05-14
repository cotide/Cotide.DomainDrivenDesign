using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Commands.UserCommands;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Enum;
using Cotide.Tests.Base;
using NUnit.Framework;

namespace Cotide.Tests.Tasks
{

    public class AdminTaskTest : TestBase
    {
        [Test]
        public void CreateUserTest()
        {
            var task = base.Get<IUserTask>();
            task.Create(new CreateUserCommand("user","user","user",UserState.Normal));

        }
    }
}
