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
        List<SaleOrderDetail> GetAllBySaleOrder(int orderId);
        List<SaleOrderDetail> GetAllByProduct(int productId);
        bool ProductExists(int productId);
        bool SaleOrderExists(int saleOrderId);
        Product? GetProduct(int productId);
    }
}
