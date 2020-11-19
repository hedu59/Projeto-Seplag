using Prototype.Domain.Commands.Input.Servidores;
using Prototype.Domain.Entities;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Application.Interfaces
{
    public interface IServidorService
    {
        ICommandResult CreateServidor(CreateBeneficioServidorCommand command);

        ICommandResult UpdateServidor(UpdateBeneficioServidorCommand command);

        ICommandResult DeleteServidor(Guid id);

        BeneficioServidor ObterTramitacoesPorID(Guid Id);
    }
}
