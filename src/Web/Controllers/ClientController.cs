using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
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

        public ClientController(ClientService service)
        {
            _service = service ;
        }

        [HttpGet("{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(_service.Get(name));
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] CreateClientRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client data.");
            }

            var client = new Client
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                UserType = request.UserType,
                Address = request.Address, 
            };
            _service.Add(client);

            return CreatedAtAction(nameof(Get), new { name = client.Name }, client);
        }

    }
}

