using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound($"No se encontró ningún Producto con el ID: {id}");
            }
            return Ok(product);
        }


        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var product = _productService.Get(name);
            if (product == null)
            {
                return NotFound($"No se encontró ningún Producto con el nombre: {name}");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductCreateRequest body)
        {
            return Ok(_productService.AddProduct(body));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                var existingProduct = _productService.Get(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró ningún Producto con el ID: {id}");
                }

                _productService.DeleteProduct(id);
                return Ok($"Producto con ID: {id} eliminado");
            }
           
            catch (Exception ex)
            {
                return BadRequest($"Se produjo un error al intentar eliminar el producto: {ex.Message}");
            }
        }
    }
}

