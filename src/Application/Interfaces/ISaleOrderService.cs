using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleOrderService
    {
        List<SaleOrder> GetAllByClient(int clientId);
        SaleOrder? GetById(int id);
        int AddSaleOrder(SaleOrderDto dto);
        void DeleteSaleOrder(int id);
        void UpdateSaleOrder(int id, SaleOrderDto dto);
    }
}