using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;

namespace Prototype.Application.Interfaces
{
    public interface IServidorService
    {
        Servidor ObterTramitacoesPorID(Guid Id);
        IPagedList<Servidor> ObterServidores(int pageIndex, int pageSize);
    }
}
