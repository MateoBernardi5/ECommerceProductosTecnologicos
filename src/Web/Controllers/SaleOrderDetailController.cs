using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleOrderDetailController : ControllerBase
    {
        private readonly ISaleOrderDetailService _saleOrderDetailService;

        public SaleOrderDetailController(ISaleOrderDetailService saleOrderDetailService)
        {
            _saleOrderDetailService = saleOrderDetailService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var saleOrderDetail = _saleOrderDetailService.Get(id);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
            }
            return Ok(saleOrderDetail);
        }
 
        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct([FromRoute] int productId)
        {
            var saleOrderDetail = _saleOrderDetailService.Get(productId);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {productId}");
            }
            return Ok(saleOrderDetail);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetAllBySaleOrder([FromRoute] int orderId)
        {
            var saleOrderDetail = _saleOrderDetailService.Get(orderId);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {orderId}");
            }
            return Ok(saleOrderDetail);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDetailDto dto)
        {
            return Ok(_saleOrderDetailService.AddSaleOrderDetail(dto));
        }

        //update

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrderDetail([FromRoute] int id)
        {
            try
            {
                var existingSaleOrderDetail = _saleOrderDetailService.Get(id);
                if (existingSaleOrderDetail == null)
                {
                    return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
                }

                _saleOrderDetailService.DeleteSaleOrderDetail(id);
                return Ok($"Detalle de venta con ID: {id} eliminada");
            }

            catch (Exception ex)
            {
                return BadRequest($"Se produjo un error al intentar eliminar el detalle de venta: {ex.Message}");
            }
        }
    }
}
