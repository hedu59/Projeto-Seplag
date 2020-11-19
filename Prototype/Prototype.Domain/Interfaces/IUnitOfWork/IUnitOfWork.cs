using Prototype.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Prototype.Domain.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection DbConnection { get; }
        void SaveChanges();
        IRepository<T> GetRepository<T>() where T : class;
    }
}
