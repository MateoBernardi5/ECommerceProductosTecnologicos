using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticateController(IConfiguration config, ICustomAuthenticationService authenticateService)
        {
            _config = config;
            _authenticationService = authenticateService;
        }

        [HttpPost]
        public ActionResult<string> Authenticate([FromBody] CredentialsDtoRequest credentials)
        {
            //string token = _authenticationService.Authenticate(credentials);
            //return Ok(token);
            try
            {
                var token = _authenticationService.Authenticate(credentials);
                return Ok(new { Token = token });
            }
            catch (NotAllowedException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
            }
        }
    }
}
