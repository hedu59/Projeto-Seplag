using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
