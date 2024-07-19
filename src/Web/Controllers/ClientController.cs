﻿using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;


namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        
        public ClientController(IClientService service)
        {
            _service = service;
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllClients());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var client = _service.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró ningún cliente con el ID: {id}");
            }
            return Ok(client);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var client = _service.Get(name);
            if (client == null)
            {
                return NotFound($"No se encontró ningún cliente con el nombre: {name}");
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientCreateRequest body)
        {
            var newClient = _service.AddClient(body);
            return Ok($"Creado el Cliente con el ID: {newClient}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient([FromRoute] int id)
        {
            var existingClient = _service.Get(id);
            if (existingClient == null)
            {
                return NotFound($"No se encontró ningún Cliente con el ID: {id}");
            }
            _service.DeleteClient(id);
            return Ok($"Cliente con ID: {id} eliminado");                
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient([FromRoute] int id, [FromBody] ClientUpdateRequest request)
        {
            var existingClient = _service.Get(id);
            if (existingClient == null)
            {
                return NotFound($"No se encontró ningún Cliente con el ID: {id}");
            }
            _service.UpdateClient(id, request);
            return Ok($"Cliente con ID: {id} actualizado correctamente");
        }
    }
}