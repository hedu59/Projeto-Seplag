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
    public  class DeleteServidorCommand : Notifiable, IRequest<ICommandResult>
    {
        public Guid ServidorId { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNull(ServidorId, "ServidorId", "O ServidorId não pode ser nulo"));
            return Valid;
        }
    }
}
