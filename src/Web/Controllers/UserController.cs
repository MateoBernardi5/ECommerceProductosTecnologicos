using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var user = _service.Get(name);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _service.Get(id);
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.Get();
            return Ok(users);
        }
    }
}
