using MediatR;

namespace Prototype.Shared.Commands
{
    public interface ICommandResult: INotification
    {
        object Data { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
