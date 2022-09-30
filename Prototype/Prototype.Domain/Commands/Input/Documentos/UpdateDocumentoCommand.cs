using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Domain.Commands.Input.Documentos
{
    public class UpdateDocumentoCommand : Notifiable, IRequest<ICommandResult>
    {
        public Guid ServidorId { get; set; }
        public Guid DocumentoId { get; set; }
        public ESetoresTramitacao Tramitacao { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotEmpty(ServidorId, "ServidorId", "O ServidorId não pode ser nulo")
            .IsNotEmpty(DocumentoId, "DocumentoId", "O DocumentoId não pode ser nulo")
            
             );
            return Valid;

        }
    }
}
