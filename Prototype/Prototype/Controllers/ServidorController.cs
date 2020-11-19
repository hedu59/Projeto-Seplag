using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.Servidores;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using System;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServidorController : ControllerBase
    {
        private readonly IServidorService _servidorService;
        private readonly IUnitOfWork _uow;

        public ServidorController(IServidorService servidor, IUnitOfWork uow)
        {
            _servidorService = servidor;
            _uow = uow;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            
            var servidores = _uow.GetRepository<BeneficioServidor>()
                .GetPagedList(predicate: x => x.Active == true, disableTracking: true, pageIndex: pageIndex, pageSize: pageSize,
                include: x => x.Include(y => y.Documentos));
           

            return Ok(servidores);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {

            var servidor = _servidorService.ObterTramitacoesPorID(Id);

            return Ok(servidor);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] CreateBeneficioServidorCommand command)
        {

            try
            {
                var result = _servidorService.CreateServidor(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateBeneficioServidorCommand command)
        {
            try
            {
                var result = _servidorService.UpdateServidor(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var result = _servidorService.DeleteServidor(id);

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