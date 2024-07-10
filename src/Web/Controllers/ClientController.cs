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
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;
        private readonly UserService _userService;
        public ClientController(ClientService service, UserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            return Ok(_service.Get(name));
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(_service.GetClients());
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientCreateRequest body)
        {
            return Ok(_service.AddClient(body));
        }

        [HttpDelete("DeleteClient/{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                var existingClient = _userService.Get(id);
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