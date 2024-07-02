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
        public IActionResult GetByName([FromRoute] string name)
        {
            return Ok(_service.Get(name));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateClientRequest body)
        {
            return Ok(_service.AddClient(body));
        }
    }
}