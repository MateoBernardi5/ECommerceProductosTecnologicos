using Application.Interfaces;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            return Forbid();
        }

        [HttpGet("by-price")]
        public IActionResult GetProductsWithMaxPrice([FromQuery] decimal price)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var products = _productService.GetProductsWithMaxPrice(price);
                if (products == null || !products.Any()) //Any() comprueba si la coleccion tiene algun elemento.
                {
                    return NotFound($"No se encontraron Productos con un precio menor o igual al ingresado.");
                }
                return Ok(products);
            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var product = _productService.Get(id);
                if (product == null)
                {
                    return NotFound($"No se encontró ningún Producto con el ID: {id}");
                }
                return Ok(product);
            }
            return Forbid();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin" || roleClaim.Value == "Client")
            {
                var product = _productService.Get(name);
                if (product == null)
                {
                    return NotFound($"No se encontró ningún Producto con el nombre: {name}");
                }
                return Ok(product);
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductCreateRequest body)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var newProduct = _productService.AddProduct(body);
                return Ok($"Creado el Producto con el ID {newProduct}");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var existingProduct = _productService.Get(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró ningún Producto con el ID: {id}");
                }
                _productService.DeleteProduct(id);
                return Ok($"Producto con ID: {id} eliminado");
            }
            return Forbid();
        }
   
        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] ProductUpdateRequest request)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim.Value == "Admin")
            {
                var existingProduct = _productService.Get(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró ningún Producto con el ID: {id}");
                }
                _productService.UpdateProduct(id, request);
                return Ok($"Producto con ID: {id} actualizado correctamente");
            }
            return Forbid();
        }
    }
}

