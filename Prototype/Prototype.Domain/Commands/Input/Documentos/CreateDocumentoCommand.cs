using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Domain.Commands.Input.Documentos
{
    public class CreateDocumentoCommand : Notifiable,  IRequest<ICommandResult>
    {
        public Guid ServidorId { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public string FileAsBase64 { get; set; }
        public byte[] FileAsByteArray { get; set; }
        public ECategoriaDocumento Categoria { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()            
            .IsNotEmpty(ServidorId, "ServidorId", "O ServidorId não pode ser nulo")
            .IsNotNull(FileAsByteArray,"Arquivo","O Arquivo não foi carregado")
            
             );
            return Valid;

        }
    }
}
