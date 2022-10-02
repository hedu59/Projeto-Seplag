using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Application.Commands.Input.Servidores
{
    public class DeleteServidorCommand : Notifiable, IRequest<ICommandResult>
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
