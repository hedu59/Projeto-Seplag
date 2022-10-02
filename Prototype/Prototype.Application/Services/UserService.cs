
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prototype.Application.Commands.Input.User;
using Prototype.Application.Handlers;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Shared.Commands;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prototype.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly UserHandler _handler;

        public UserService(IUnitOfWork uow , IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public ICommandResult AuthenticationUser(string login, string password, string email)
        {
           
            var user = _uow.GetRepository<User>().GetFirstOrDefault(predicate: x => x.Login == login && x.Password == password);

            if(user != null)
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login)

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenConfig:HashKey").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                (
                    issuer: "prototype",
                    audience: "prototype",
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: creds

                );

                var tokenResult = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

                return new CommandResult(true, "Credenciais Validas", tokenResult); ;
            }

            return new CommandResult(true, "Credenciais inválidas", new { Propriety = "LOGIN ou SENHA", Message = "Login ou Senha invalidos!" });
        }

        public ICommandResult CreateUserDefault()
        { 
            return _handler.Handle();
        }

        public ICommandResult CreateUser(CreateUserCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);

        }

        public ICommandResult UpdateUser(UpdateUserCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);
        }

        public ICommandResult DeleteBarber(Guid id)
        {
            return _handler.Handle(id);
        }

        
    }
}
