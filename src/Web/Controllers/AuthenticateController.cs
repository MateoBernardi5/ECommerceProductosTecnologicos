using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public IUserService _userService;
        public IConfiguration _configuration;

        public AuthenticateController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult <string> Authenticate([FromBody] CredentialsDtoRequest credentials)
        {
            string token = _userService.Authenticate(credentials);
            return Ok(token);
        }
    }
}
