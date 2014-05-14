using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Commands.ClientAuthorization
{
    public class DeleteTokenCommand
    {
        public readonly string Code;

        public DeleteTokenCommand(string code)
        {
            Code = code;
        }
    }
}
