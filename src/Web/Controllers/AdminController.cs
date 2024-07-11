using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;
        private readonly UserService _userService;
        public AdminController(AdminService service, UserService userService)
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
            try
            {
                var existingAdmin = _service.Get(id);
                if (existingAdmin == null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }

                _userService.DeleteUser(id);
                return Ok($"Admin con ID: {id} eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Se produjo un error al intentar eliminar el admin: {ex.Message}");
            }
        }
    }
}

