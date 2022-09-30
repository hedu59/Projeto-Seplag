using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Commands.Input.Servidores
{
    public class CreateServidorCommand : Notifiable, IRequest<ICommandResult>
    {
        public string Nome { get;  set; }
        public string CPF { get;  set; }
        public string Orgao { get;  set; }
        public string Matricula { get;  set; }
        public ESetoresTramitacao SetorTramitacao { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .HasMaxLen(Nome, 80, "Nome", "O nome não pode ter mais de 80 caracteres")
            .HasMinLen(Nome, 5, "Nome", "O nome não pode ter menos de 5 caracteres")
            .IsNotNullOrEmpty(Nome, "Nome", "O nome não pode ser nulo")

            .HasExactLengthIfNotNullOrEmpty(CPF, 11, "CPF", "O CPF deve conter 11 caracteres")
            .IsNotNullOrEmpty(CPF, "CPF", "O CPF não pode ser nulo")

            .HasMaxLen(Orgao, 80, "Orgao", "O Orgao não pode ter mais de 80 caracteres")
            .HasMinLen(Orgao, 1, "Orgao", "O Orgao não pode ter menos de 1 caracteres")
            .IsNotNullOrEmpty(Orgao, "Orgao", "O Orgao não pode ser nulo")

            .HasMaxLen(Matricula, 40, "Matricula", "A Matricula não pode ter mais de 40 caracteres")
            .HasMinLen(Matricula, 5, "Matricula", "A Matricula não pode ter menos de 5 caracteres")
            .IsNotNullOrEmpty(Matricula, "Matricula", "A Matricula não pode ser nulo")

                );
            return Valid;
        }
    }
}
