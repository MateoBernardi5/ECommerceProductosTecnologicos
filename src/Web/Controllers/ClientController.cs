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
            // Obtener el claim de rol, si existe
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            // Verificar si el claim existe y su valor es "Admin"
            if (roleClaim != null && roleClaim.Value == "Admin")
            {
                return Ok(_service.GetAllClients());
            }
            // Si el rol no es Admin o el claim no existe, prohibir acceso
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var client = _service.Get(id);
                if (client == null)
                {
                    return NotFound($"No se encontró ningún cliente con el ID: {id}");
                }
                return Ok(client);
            }
            return Forbid();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var client = _service.Get(name);
                if (client == null)
                {
                    return NotFound($"No se encontró ningún cliente con el nombre: {name}");
                }
                return Ok(client);
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientCreateRequest body)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var newClient = _service.AddClient(body);
                return Ok($"Creado el Cliente con el ID: {newClient}");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var existingClient = _service.Get(id);
                if (existingClient == null)
                {
                    return NotFound($"No se encontró ningún Cliente con el ID: {id}");
                }
                _service.DeleteClient(id);
                return Ok($"Cliente con ID: {id} eliminado");
            }
            return Forbid();               
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient([FromRoute] int id, [FromBody] ClientUpdateRequest request)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var existingClient = _service.Get(id);
                if (existingClient == null)
                {
                    return NotFound($"No se encontró ningún Cliente con el ID: {id}");
                }
                _service.UpdateClient(id, request);
                return Ok($"Cliente con ID: {id} actualizado correctamente");
            }
            return Forbid();
        }
    }
}