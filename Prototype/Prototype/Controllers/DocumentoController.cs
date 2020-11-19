using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.Documentos;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using System;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IDocumentoService _service;
        public DocumentoController(IDocumentoService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }


        [HttpGet()]
        public IActionResult GetByServidorId(Guid servidorId, int pageIndex, int pageSize)
        {
            var documento = _service.ObterListDeDocumentoPorServidor(servidorId, pageIndex, pageSize);

            return Ok(documento);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] CreateDocumentoCommand command)
        {

            try
            {
                var result = _service.CreateDocumento(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public IActionResult Update([FromBody] UpdateDocumentoCommand command)
        {

            try
            {
                var result = _service.UpdateDocumento(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}