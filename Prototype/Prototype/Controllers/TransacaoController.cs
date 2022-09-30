using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoMongoRepository _repository;
        public TransacaoController(ITransacaoMongoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTramitacoes()
        => Ok(await _repository.GetAllAsync());

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTramitacao(string Id)
        => Ok(await _repository.GetByIdAsync(Id));
    }
}
