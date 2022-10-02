using Microsoft.AspNetCore.Mvc;
using Prototype.Shared.Commands;
namespace Prototype.Domain.Commands.Output
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
