using Flunt.Notifications;
using Flunt.Validations;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Commands.Input.Servidores
{
    public class UpdateBeneficioServidorCommand: Notifiable, ICommand
    {
        public Guid ServidorId { get;   set; }

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
