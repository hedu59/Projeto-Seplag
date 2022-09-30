using Microsoft.EntityFrameworkCore;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Repositories.Interfaces;
using Prototype.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Prototype.Infra.Data.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private bool disposed = false;
        private readonly PrototypeDataContext _context;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(PrototypeDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            DbConnection.Open();
        }

        public IDbConnection DbConnection => _context.Database.GetDbConnection();

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Dictionary<Type, object>();

            var type = typeof(T);

            if (!repositories.ContainsKey(type))
                repositories[type] = new Repository<T>(_context);

            return (IRepository<T>)repositories[type];
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }

                    DbConnection.Close();
                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}
