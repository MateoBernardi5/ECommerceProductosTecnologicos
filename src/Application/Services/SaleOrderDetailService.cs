using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
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
        public SaleOrderDetailService(ISaleOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public List<SaleOrderDetail> GetAllSaleOrderDetails()
        {
            return _repository.Get();
        }

        public List<SaleOrderDetail> GetAllByProduct(int productId)
        {
            return _repository.GetAllByProduct(productId);
        }

        public List<SaleOrderDetail> GetAllBySaleOrder(int orderId)
        {
            //return _repository.Get(orderId);
            return _repository.GetAllBySaleOrder(orderId); ; // Método actualizado
        }

        public SaleOrderDetail? Get(int id)
        {
            return _repository.Get(id);
        }

        public int AddSaleOrderDetail(SaleOrderDetailDto dto)
        {
            // Verifica que el ProductId exista en la tabla de productos
            if (!_repository.ProductExists(dto.ProductId))
            {
                throw new Exception("ProductId no existe.");
            }

            // Verifica que el SaleOrderId exista en la tabla de órdenes de venta
            if (!_repository.SaleOrderExists(dto.SaleOrderId))
            {
                throw new Exception("SaleOrderId no existe.");
            }

            // Obtén el producto para asegurarte de que no sea nulo
            var product = _repository.GetProduct(dto.ProductId);
            if (product == null)
            {
                throw new Exception("El producto no se pudo encontrar.");
            }

            var saleOrderLine = new SaleOrderDetail()
            {
                ProductId = dto.ProductId,
                SaleOrderId = dto.SaleOrderId,
                Amount = dto.Amount,
                UnitPrice = product.Price, // Asigna el precio unitario del producto
                Product = product // Asigna el producto para evitar referencias nulas
            };
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
