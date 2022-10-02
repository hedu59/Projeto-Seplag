using Prototype.Application.Commands.Input.User;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Application.Interfaces
{
    public interface IUserService
    {
        ICommandResult AuthenticationUser(string login, string password, string email);

        ICommandResult CreateUserDefault();

        ICommandResult CreateUser(CreateUserCommand command);

        ICommandResult UpdateUser(UpdateUserCommand command);

        ICommandResult DeleteBarber(Guid id);
    }
}
