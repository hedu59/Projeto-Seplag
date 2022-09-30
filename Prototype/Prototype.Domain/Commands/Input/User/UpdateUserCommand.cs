using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Domain.Commands.Input.User
{
    public class UpdateUserCommand : Notifiable, ICommand, IRequest<string>
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public bool Validate()
        {
            AddNotifications(new Contract()

                .Requires()
                .IsNullOrEmpty(Login, "Login", "Defina o seu login de usuario")
                .HasMinLen(Login, 5, "Login", "O Login não pode ter menos de 5 caracteres.")
                .HasMaxLen(Login, 50, "Login", "O Login não pode ter mais que 50 caracteres.")

                .IsEmail(Email, "Email", Email + " - Não é um email válido")

                .IsNotNullOrEmpty(Password, "Senha", "A senha não pode ser vazia")
                .HasMinLen(Password, 6, "Senha", "A senha deve ter pelo menos 6 carateres")
                .HasMaxLen(Password, 15, "Senha", "A senha não poder ter mais 15 carateres")

                .IsNotEmpty(UserId, "UserId", "Informe o Id do usuário")

            );
           return Valid;
        }
    }
}
