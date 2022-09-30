using Prototype.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Domain.Interfaces.Repositories
{
    public  interface ITransacaoMongoRepository
    {
        Task<List<LogTransacao>> GetAllAsync();
        Task<LogTransacao> GetByIdAsync(string id);
        Task<LogTransacao> CreateAsync(LogTransacao log);
    }
}
