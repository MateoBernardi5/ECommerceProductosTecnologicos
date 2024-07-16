using Application.Interfaces;
using Application.Models;
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
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var saleOrders = _saleOrderService.GetAllSaleOrders();
            return Ok(saleOrders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var saleOrder = _saleOrderService.Get(id);
            if (saleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {id}");
            }
            return Ok(saleOrder);
        }


        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDto dto)
        {
            return Ok(_saleOrderService.AddSaleOrder(dto));
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder([FromRoute] int id)
        {
            try
            {
                var existingSaleOrder = _saleOrderService.Get(id);
                if (existingSaleOrder == null)
                {
                    return NotFound($"No se encontró ninguna venta con el ID: {id}");
                }

                _saleOrderService.DeleteSaleOrder(id);
                return Ok($"Venta con ID: {id} eliminada");
            }

            catch (Exception ex)
            {
                return BadRequest($"Se produjo un error al intentar eliminar la venta: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderDto dto)
        {
            // Verificar si existe el Admin con el ID proporcionado
            var existingSaleOrder = _saleOrderService.Get(id);
            if (existingSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna Venta con el ID: {id}");
            }

            // Actualizar el Admin
            _saleOrderService.UpdateSaleOrder(id, dto);
            return Ok($"Venta con ID: {id} actualizado correctamente");
        }
    }
}
