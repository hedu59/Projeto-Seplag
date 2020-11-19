﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.User;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

    
        [AllowAnonymous]
        [HttpPost("CreateUserDefault")]
        public IActionResult CreateUserDefault()
        {

            var validar = _userService.CreateUserDefault();
            return Ok(validar);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserCommand command)
        {

            try
            {
                var result = _userService.CreateUser(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserCommand command)
        {
            try
            {
                var result = _userService.UpdateUser(command);

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
                var result = _userService.DeleteBarber(id);

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