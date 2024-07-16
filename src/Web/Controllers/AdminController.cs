using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly IUserService _userService;
        public AdminController(IAdminService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllAdmins());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var admin = _service.Get(id);
            if (admin == null)
            {
                return NotFound($"No se encontró ningún admin con el ID: {id}");
            }
            return Ok(admin);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var admin = _service.Get(name);
            if (admin == null)
            {
                return NotFound($"No se encontró ningún admin con el nombre: {name}");
            }
            return Ok(admin);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AdminCreateRequest body)
        {
            return Ok(_service.AddAdmin(body));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var existingAdmin = _service.Get(id);
            if (existingAdmin == null)
            {
                return NotFound($"No se encontró ningún Admin con el ID: {id}");
            }
            _service.DeleteAdmin(id);
            return Ok($"Admin con ID: {id} eliminado");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAdmin([FromRoute] int id, [FromBody] AdminUpdateRequest request)
        {
            // Verificar si existe el Admin con el ID proporcionado
            var existingAdmin = _service.Get(id);
            if (existingAdmin == null)
            {
                return NotFound($"No se encontró ningún Admin con el ID: {id}");
            }

            // Actualizar el Admin
            _service.UpdateAdmin(id, request);
            return Ok($"Admin con ID: {id} actualizado correctamente");
        }
    }
}

