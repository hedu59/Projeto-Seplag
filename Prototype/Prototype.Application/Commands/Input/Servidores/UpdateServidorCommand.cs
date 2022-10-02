using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Application.Commands.Input.Servidores
{
    public class UpdateServidorCommand : Notifiable, IRequest<ICommandResult>
    {
        public Guid ServidorId { get; set; }

        public ESetoresTramitacao Tramitacao { get; set; }


        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNull(ServidorId, "ServidorId", "O ServidorId não pode ser nulo")
            .IsNotNull(Tramitacao, "Tramitacao", "A Tramitacao não pode ser nula"));
            return Valid;
        }
    }
}
