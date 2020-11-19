using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared.Commands
{
    public interface ICommandResult
    {
        object Data { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
