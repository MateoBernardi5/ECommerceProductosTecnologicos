using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService service)
        {
            _service = service ;
        }

        [HttpGet("{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(_service.Get(name));
        }
    }
}
