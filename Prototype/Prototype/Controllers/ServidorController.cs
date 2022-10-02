using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Commands.Input.Servidores;
using Prototype.Application.Interfaces;
using Prototype.Domain.Interfaces.IUnitOfWork;
using System;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServidorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IServidorService _servidorService;
        private readonly IUnitOfWork _uow;

        public ServidorController(IServidorService servidor, IUnitOfWork uow, IMediator mediator)
        {
            _servidorService = servidor;
            _uow = uow;
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(int? pageIndex, int? pageSize)
         => Ok(_servidorService.ObterServidores(pageIndex ?? 1, pageSize ?? 10));
        
        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        => Ok( _servidorService.ObterTramitacoesPorID(Id));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateServidorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateServidorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete ]
        public async Task<IActionResult> Delete(DeleteServidorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}