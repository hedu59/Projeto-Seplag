using Prototype.Domain.Commands.Input.User;
using Prototype.Domain.Entities;
using Prototype.Shared;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

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
