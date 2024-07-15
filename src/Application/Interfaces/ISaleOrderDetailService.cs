using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleOrderDetailService
    {
        List<SaleOrderDetail> GetAllByProduct(int productId);
        List<SaleOrderDetail> GetAllBySaleOrder(int orderId);
        SaleOrderDetail? Get(int id);
        int AddSaleOrderDetail(SaleOrderDetailDto dto);
        void DeleteSaleOrderDetail(int id);
    }
}
