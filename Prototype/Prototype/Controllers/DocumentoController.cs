using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.Documentos;
using Prototype.Domain.Interfaces.IUnitOfWork;
using System;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IDocumentoService _service;
        private readonly IMediator _mediator;
        public DocumentoController(IDocumentoService service, IUnitOfWork uow, IMediator mediator)
        {
            _service = service;
            _uow = uow;
            _mediator = mediator;
        }


        [HttpGet()]
        public IActionResult GetByServidorId(Guid servidorId, int pageIndex, int pageSize)
        {
            var documento = _service.ObterListDeDocumentoPorServidor(servidorId, pageIndex, pageSize);

            return Ok(documento);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>Create([FromBody] CreateDocumentoCommand command)
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
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentoCommand command)
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
    }
}