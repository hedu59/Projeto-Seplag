using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;

namespace Prototype.Application.Interfaces
{
    public interface IDocumentoService
    {
      
        IPagedList<Documento> ObterListDeDocumento(int pageIndex, int pageSize);

        IPagedList<Documento> ObterListDeDocumentoPorServidor(Guid ServidorId, int pageIndex, int pageSize);

        Documento ObterDocumentoPorID(Guid Id);

    }
}
