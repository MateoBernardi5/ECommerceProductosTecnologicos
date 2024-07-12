using Application.Interfaces;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        private readonly IUserService _userService;
        public ClientController(IClientService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
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
            return Ok(_service.AddClient(body));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient([FromRoute] int id)
        {
            try
            {
                var existingClient = _service.Get(id);
                if (existingClient == null)
                {
                    return NotFound($"No se encontró ningún Cliente con el ID: {id}");
                }

                _userService.DeleteUser(id);
                return Ok($"Cliente con ID: {id} eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Se produjo un error al intentar eliminar el cliente: {ex.Message}");
            }
        }
    }
}