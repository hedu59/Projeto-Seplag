using Flunt.Notifications;
using Prototype.Application.Commands.Input.User;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Application.Handlers
{
    public class UserHandler : Notifiable,
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _uow;

        public UserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICommandResult Handle(CreateUserCommand command)
        {
            try
            {
                _uow.GetRepository<User>()
                .Save(new User(email: command.Email, login: command.Login, password: command.Password));

                _uow.SaveChanges();
                return new CommandResult(success: true, message: "Usuário salvo com sucesso", data: command);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public ICommandResult Handle(UpdateUserCommand command)
        {
            try
            {
                var user = _uow
                    .GetRepository<User>()
                    .GetFirstOrDefault(predicate: x => x.Id == command.UserId);

                if (user != null)
                {
                    user.UpdateUser(login: command.Login, email: command.Email, password: command.Password);

                    _uow.GetRepository<User>().Update(entity: user);

                    _uow.SaveChanges();

                    return new CommandResult(success: true, message: "Usuário alterado com sucesso", data: command);
                }

                return new CommandResult(success: false, message: "Usuário nao encontrada", data: command);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public ICommandResult Handle(Guid id)
        {
            try
            {
                var user = _uow.GetRepository<User>().GetFirstOrDefault(predicate: x => x.Id == id);

                user.Disable();

                _uow.GetRepository<User>().Update(entity: user);

                _uow.SaveChanges();

                return new CommandResult(success: true, message: "Usuário removido com sucesso", data: null);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public ICommandResult Handle()
        {
            try
            {
                _uow.GetRepository<User>()
                .Save(new User(email: "admin@admin.com", login: "admin", password: "123456"));

                _uow.SaveChanges();
                return new CommandResult(success: true, message: "Usuário salvo com sucesso", data: new User("admin", "123456", "admin@admin.com") { });
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }
    }
}
