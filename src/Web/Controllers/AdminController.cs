using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        
        public AdminController(IAdminService service)
        {
            _service = service;
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                return Ok(_service.GetAllAdmins());
            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var admin = _service.Get(id);
                if (admin == null)
                {
                    return NotFound($"No se encontró ningún admin con el ID: {id}");
                }
                return Ok(admin);
            }
            return Forbid();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var admin = _service.Get(name);
                if (admin == null)
                {
                    return NotFound($"No se encontró ningún admin con el nombre: {name}");
                }
                return Ok(admin);
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] AdminCreateRequest body)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var newAdmin = _service.AddAdmin(body);
                return Ok($"Creado el Admin con el ID: {newAdmin}");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var existingAdmin = _service.Get(id);
                if (existingAdmin == null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }
                _service.DeleteAdmin(id);
                return Ok($"Admin con ID: {id} eliminado");
            }
            return Forbid();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAdmin([FromRoute] int id, [FromBody] AdminUpdateRequest request)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var existingAdmin = _service.Get(id);
                if (existingAdmin == null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }
                _service.UpdateAdmin(id, request);
                return Ok($"Admin con ID: {id} actualizado correctamente");
            }
            return Forbid();
        }
    }
}

