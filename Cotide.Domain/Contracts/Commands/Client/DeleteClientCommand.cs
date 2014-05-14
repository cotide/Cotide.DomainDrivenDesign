using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Commands.Client
{
    public class DeleteClientCommand
    {
        public Guid Id;

        public DeleteClientCommand(Guid id)
        {
           
            Id = id;
        }
    }
}
