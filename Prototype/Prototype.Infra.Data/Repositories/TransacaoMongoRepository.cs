using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.Repositories;
using Prototype.Infra.Data.MongoConfiguration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Prototype.Infra.Data.Repositories
{


    public class TransacaoMongoRepository : ITransacaoMongoRepository
    {
        private readonly IMongoCollection<LogTransacao> _collection;
        private readonly DbConfiguration _settings;

        public TransacaoMongoRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<LogTransacao>(_settings.CollectionName);
        }

        public async Task<LogTransacao> CreateAsync(LogTransacao log)
        {
            try
            {
                await _collection.InsertOneAsync(log);
                return log;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex.InnerException);
            }          
        }

        public Task<List<LogTransacao>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }

        public Task<LogTransacao> GetByIdAsync(string id)
        {
            Expression<Func<LogTransacao, bool>> filter = ObterFilter(id);

            var servidor =  _collection.Find(filter).FirstOrDefaultAsync();

            return servidor;
        }

        private static Expression<Func<LogTransacao, bool>> ObterFilter(string id)
        {
            return x => x.Id.Equals(ObjectId.Parse(id));
        }
    }
}
