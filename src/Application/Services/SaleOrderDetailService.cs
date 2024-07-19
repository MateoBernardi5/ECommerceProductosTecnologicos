using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SaleOrderDetailService : ISaleOrderDetailService
    {
        private readonly ISaleOrderDetailRepository _repository;
        private readonly IProductService _productService;
        public SaleOrderDetailService(ISaleOrderDetailRepository repository, IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        public List<SaleOrderDetail> GetAllByClient(int clientId)
        {
            return _repository.GetAllByClient(clientId);
        }

        public List<SaleOrderDetail> GetAllByProduct(int productId)
        {
            return _repository.GetAllByProduct(productId);
        }

        public List<SaleOrderDetail> GetAllBySaleOrder(int orderId)
        {
            return _repository.GetAllBySaleOrder(orderId); ; // Método actualizado
        }

        public SaleOrderDetail? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int AddSaleOrderDetail(SaleOrderDetailDto dto)
        {
            // Verifica que el ProductId exista en la tabla de productos
            //if (!_repository.ProductExists(dto.ProductId))
            //{
            //    throw new NotAllowedException("ProductId no existe.");
            //}

            // Obtén el producto para asegurarte de que no sea nulo
            var product = _repository.GetProduct(dto.ProductId);
            if (product == null)
            {
                throw new NotAllowedException("El producto no se pudo encontrar.");
            }

            // Verifica que el SaleOrderId exista en la tabla de órdenes de venta
            if (!_repository.SaleOrderExists(dto.SaleOrderId))
            {
                throw new NotAllowedException("SaleOrderId no existe.");
            }

            // Verifica el stock del producto
            if (product.Stock <= 0)
            {
                throw new NotAllowedException("El producto no está disponible en stock.");
            }

            if (product.Stock < dto.Amount)
            {
                throw new NotAllowedException("No hay suficiente stock para el producto.");
            }

            var saleOrderLine = new SaleOrderDetail()
            {
                ProductId = dto.ProductId,
                SaleOrderId = dto.SaleOrderId,
                Amount = dto.Amount,
                UnitPrice = product.Price, // Asigna el precio unitario del producto
                Product = product // Asigna el producto para evitar referencias nulas
            };

            // Actualiza el stock del producto
            var updatedProductRequest = new ProductUpdateRequest
            {
                Price = product.Price,
                Stock = product.Stock - dto.Amount
            };
            _productService.UpdateProduct(dto.ProductId, updatedProductRequest);

            // Verificar y actualizar el estado del stock
            product.Stock = updatedProductRequest.Stock;
            var stockStatus = product.StockStatus;

            return _repository.Add(saleOrderLine).Id;
        }


        public void DeleteSaleOrderDetail(int id)
        {
            var saleOrderDetailToDelete = _repository.Get(id);
            if (saleOrderDetailToDelete != null)
            {
                _repository.Delete(saleOrderDetailToDelete);
            }
        }

        public void UpdateSaleOrderDetail(int id, SaleOrderDetailUpdateRequest request)
        {
            var saleOrderDetailToUpdate = _repository.Get(id);
            if (saleOrderDetailToUpdate != null)
            {
               saleOrderDetailToUpdate.Amount = request.Amount;
               saleOrderDetailToUpdate.ProductId = request.ProductId;


                _repository.Update(saleOrderDetailToUpdate);
            }
        }
    }
}
