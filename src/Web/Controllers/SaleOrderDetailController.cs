using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
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
    public class SaleOrderDetailController : ControllerBase
    {
        private readonly ISaleOrderDetailService _saleOrderDetailService;
        private readonly ISaleOrderService _saleOrderService;
        private readonly IProductService _productService;

        public SaleOrderDetailController(ISaleOrderDetailService saleOrderDetailService, ISaleOrderService saleOrderService, IProductService productService)
        {
            _saleOrderDetailService = saleOrderDetailService;
            _saleOrderService = saleOrderService;
            _productService = productService;
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

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrderDetail = _saleOrderDetailService.GetById(id);
            if (saleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
            }

            var saleOrder = _saleOrderService.GetById(saleOrderDetail.SaleOrderId);
            if (saleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {saleOrderDetail.SaleOrderId}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == saleOrder.ClientId))
            {
                return Ok(saleOrderDetail);
            }

            return Forbid();
        }

        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct([FromRoute] int productId)
        {
            if (IsUserInRole("Admin"))
            {
                var product = _productService.Get(productId);
                if (product == null)
                {
                    return NotFound($"No se encontró ningún producto con el ID: {productId}");
                }

                var saleOrderDetails = _saleOrderDetailService.GetAllByProduct(productId);
                return Ok(saleOrderDetails);
            }
            return Forbid();
        }

        [HttpGet("{orderId}")]
        public IActionResult GetAllBySaleOrder([FromRoute] int orderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrder = _saleOrderService.GetById(orderId);
            if (saleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {orderId}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == saleOrder.ClientId))
            {
                var saleOrderDetails = _saleOrderDetailService.GetAllBySaleOrder(orderId);
                return Ok(saleOrderDetails);
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDetailDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var existingSaleOrder = _saleOrderService.GetById(dto.SaleOrderId);
            if (existingSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {dto.SaleOrderId}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == existingSaleOrder.ClientId))
            {
                var saleOrderDetailId = _saleOrderDetailService.AddSaleOrderDetail(dto);
                return CreatedAtAction(nameof(GetById), new { id = saleOrderDetailId }, $"Creado el Detalle de Venta con el ID: {saleOrderDetailId}");
            }
            return Forbid();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrderDetail([FromRoute] int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var existingSaleOrderDetail = _saleOrderDetailService.GetById(id);
            if (existingSaleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun detalle de venta con el ID: {id}");
            }

            var existingSaleOrder = _saleOrderService.GetById(existingSaleOrderDetail.SaleOrderId);
            if (existingSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {existingSaleOrderDetail.SaleOrderId}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == existingSaleOrder.ClientId))
            {
                _saleOrderDetailService.DeleteSaleOrderDetail(id);
                return Ok($"Detalle de venta con ID: {id} eliminado");
            }

            return Forbid();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrderDetail([FromRoute] int id, [FromBody] SaleOrderDetailUpdateRequest request)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var existingSaleOrderDetail = _saleOrderDetailService.GetById(id);
            if (existingSaleOrderDetail == null)
            {
                return NotFound($"No se encontró ningun Detalle de Venta con el ID: {id}");
            }

            var existingSaleOrder = _saleOrderService.GetById(existingSaleOrderDetail.SaleOrderId);
            if (existingSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {existingSaleOrderDetail.SaleOrderId}");
            }

            if (IsUserInRole("Admin") || (IsUserInRole("Client") && userId == existingSaleOrder.ClientId))
            {
                try
                {
                    _saleOrderDetailService.UpdateSaleOrderDetail(id, request);
                    return Ok($"Detalle de Venta con ID: {id} actualizado correctamente");
                }
                catch (NotAllowedException ex )
                {
                    return NotFound(ex.Message);
                }           
            }

            return Forbid();
        }

    }
}
