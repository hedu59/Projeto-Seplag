using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared.Commands
{
    public interface ICommand
    {
        bool Validate();
    }
}
