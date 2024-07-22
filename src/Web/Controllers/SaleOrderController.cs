using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        private bool IsUserInRole(string role)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role); // Obtener el claim de rol, si existe
            return roleClaim != null && roleClaim.Value == role; //Verificar si el claim existe y su valor es "role"
        }
        private int? GetUserId() //Funcion para obtener el userId de las claims del usuario autenticado en el contexto de la solicitud actual.
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }


        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == clientId))
            {
                var saleOrders = _saleOrderService.GetAllByClient(clientId);
                return Ok(saleOrders);
            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (IsUserInRole("Admin"))
            {
                var saleOrder = _saleOrderService.GetById(id);
                if (saleOrder == null)
                {
                    return NotFound($"No se encontró ninguna venta con el ID: {id}");
                }
                return Ok(saleOrder);
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == dto.ClientId))
            {
                var saleOrder = _saleOrderService.AddSaleOrder(dto);
                return CreatedAtAction(nameof(GetById), new { id = saleOrder }, $"Creada la Venta con el ID: {saleOrder}");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder([FromRoute] int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            var existingSaleOrder = _saleOrderService.GetById(id);
            if (existingSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {id}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == existingSaleOrder.ClientId))
            {
                _saleOrderService.DeleteSaleOrder(id);
                return Ok($"Venta con ID: {id} eliminada");
            }

            return Forbid();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderDto dto)
        {
            if (IsUserInRole("Admin"))
            {
                // Verificar si existe el Admin con el ID proporcionado
                var existingSaleOrder = _saleOrderService.GetById(id);
                if (existingSaleOrder == null)
                {
                    return NotFound($"No se encontró ninguna Venta con el ID: {id}");
                }

                // Actualizar el Admin
                _saleOrderService.UpdateSaleOrder(id, dto);
                return Ok($"Venta con ID: {id} actualizado correctamente");
            }
            return Forbid();
        }
    }
}
