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
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _repository;

        public SaleOrderService(ISaleOrderRepository repository)
        {
            _repository = repository;
        }
        public List<SaleOrder> GetAllByClient(int clientId)
        {
            return _repository.GetAllByClient(clientId);
        }

        public SaleOrder? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int AddSaleOrder(SaleOrderDto dto)
        {
            var saleOrder = new SaleOrder()
            {
                ClientId = dto.ClientId,
            };
            return _repository.Add(saleOrder).Id;
        }

       
        public void DeleteSaleOrder(int id)
        {
            var saleOrderToDelete = _repository.Get(id);
            if (saleOrderToDelete != null)
            {
                _repository.Delete(saleOrderToDelete);
            }
        }

        public void UpdateSaleOrder(int id, SaleOrderDto dto)
        {
            var saleOrderToUpdate = _repository.Get(id);
            if (saleOrderToUpdate != null)
            {
                saleOrderToUpdate.ClientId = dto.ClientId;
                _repository.Update(saleOrderToUpdate);
            }
        }
    }
}
