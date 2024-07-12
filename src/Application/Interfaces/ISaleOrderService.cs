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
        List<SaleOrder> GetAllSaleOrders();
        SaleOrder? Get(int id);
        int AddSaleOrder(SaleOrderDto dto);
        void DeleteSaleOrder(int id);
    }
}