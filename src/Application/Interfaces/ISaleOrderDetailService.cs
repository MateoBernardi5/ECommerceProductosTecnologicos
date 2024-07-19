using Application.Models;
using Application.Models.Requests;
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
        List<SaleOrderDetail> GetAllByClient(int clientId);
        List<SaleOrderDetail> GetAllByProduct(int productId);
        List<SaleOrderDetail> GetAllBySaleOrder(int orderId);
        SaleOrderDetail? GetById(int id);
        int AddSaleOrderDetail(SaleOrderDetailDto dto);
        void DeleteSaleOrderDetail(int id);
        void UpdateSaleOrderDetail(int id, SaleOrderDetailUpdateRequest request);
    }
}
