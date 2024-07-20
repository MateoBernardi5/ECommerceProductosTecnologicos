using Application.Interfaces;
using Application.Models;
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
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var saleOrders = _saleOrderService.GetAllByClient(clientId);
                return Ok(saleOrders);
            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
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
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var saleOrder = _saleOrderService.AddSaleOrder(dto);
                return Ok($"Creada la Venta con el ID: {saleOrder}");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var existingSaleOrder = _saleOrderService.GetById(id);
                if (existingSaleOrder == null)
                {
                    return NotFound($"No se encontró ninguna venta con el ID: {id}");
                }
                _saleOrderService.DeleteSaleOrder(id);
                return Ok($"Venta con ID: {id} eliminada");
            }
            return Forbid();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderDto dto)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
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
