using Prototype.Domain.Commands.Input.Documentos;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Application.Interfaces
{
    public interface IDocumentoService
    {
        ICommandResult CreateDocumento(CreateDocumentoCommand command);

        ICommandResult UpdateDocumento(UpdateDocumentoCommand command);

        ICommandResult DeleteDocumento(Guid id);

        IPagedList<Documento> ObterListDeDocumento(int pageIndex, int pageSize);

        IPagedList<Documento> ObterListDeDocumentoPorServidor(Guid ServidorId, int pageIndex, int pageSize);

        Documento ObterDocumentoPorID(Guid Id);

    }
}
