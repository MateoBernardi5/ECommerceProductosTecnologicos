using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleOrderDetailRepository : IBaseRepository<SaleOrderDetail>
    {
        SaleOrderDetail? GetById(int id);
        List<SaleOrderDetail> GetAllBySaleOrder(int orderId);
        List<SaleOrderDetail> GetAllByProduct(int productId);
        List<SaleOrderDetail> GetAllByClient(int clientId);
        //bool ProductExists(int productId);
        bool SaleOrderExists(int saleOrderId);
        Product? GetProduct(int productId);
    }
}
