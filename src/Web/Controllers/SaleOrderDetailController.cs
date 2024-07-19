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
            var saleOrderDetail = _saleOrderDetailService.GetById(id);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
            }
            return Ok(saleOrderDetail);
        }
 
        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct([FromRoute] int productId)
        {
            var saleOrderDetail = _saleOrderDetailService.GetAllByProduct(productId);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {productId}");
            }
            return Ok(saleOrderDetail);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetAllBySaleOrder([FromRoute] int orderId)
        {
            var saleOrderDetail = _saleOrderDetailService.GetAllBySaleOrder(orderId);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {orderId}");
            }
            return Ok(saleOrderDetail);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDetailDto dto)
        {
            var saleOrderDetail = _saleOrderDetailService.AddSaleOrderDetail(dto);
            return Ok($"Creado el Detalle de Venta con el ID: {saleOrderDetail}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrderDetail([FromRoute] int id)
        {
            var existingSaleOrderDetail = _saleOrderDetailService.GetById(id);
            if (existingSaleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
            }
            _saleOrderDetailService.DeleteSaleOrderDetail(id);
            return Ok($"Detalle de venta con ID: {id} eliminada");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrderDetail([FromRoute] int id, [FromBody] SaleOrderDetailUpdateRequest request)
        {
            // Verificar si existe el Admin con el ID proporcionado
            var existingSaleOrderDetail = _saleOrderDetailService.GetById(id);
            if (existingSaleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun Detalle de Venta con el ID: {id}");
            }

            // Actualizar el Admin
            _saleOrderDetailService.UpdateSaleOrderDetail(id, request);
            return Ok($"Detalle de Venta con ID: {id} actualizado correctamente");
        }
    }
}
