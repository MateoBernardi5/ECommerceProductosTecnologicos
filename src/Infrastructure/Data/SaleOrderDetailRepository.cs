using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SaleOrderDetailRepository : BaseRepository<SaleOrderDetail>, ISaleOrderDetailRepository
    {
        private readonly ApplicationContext _context;
        public SaleOrderDetailRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public List<SaleOrderDetail> GetAllBySaleOrder(int orderId)
        {
            return _context.SaleOrderDetails
                .Include(sol => sol.Product)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .Where(sol => sol.SaleOrderId == orderId)
                .ToList();
        }

        public List<SaleOrderDetail> GetAllByProduct(int productId)
        {
            return _context.SaleOrderDetails
                .Include(sol => sol.Product)
                .Where(sol => sol.ProductId == productId)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public bool SaleOrderExists(int saleOrderId)
        {
            return _context.SaleOrders.Any(s => s.Id == saleOrderId);
        }
        public Product? GetProduct(int productId)
        {
            return _context.Products.Find(productId);
        }

        //public SaleOrderDetail? GetOne(int Id)
        //{
        //    return _context.SaleOrdersDetails
        //        .Include(sol => sol.Product)
        //        .Include(sol => sol.SaleOrder)
        //        .ThenInclude(so => so.Client)
        //        .SingleOrDefault(x => x.Id == Id);
        //}

        //public SaleOrderDetail CreateSaleOrderLine(SaleOrderDetail lineOfOrder)
        //{
        //    _context.Add(lineOfOrder);
        //    _context.SaveChanges();
        //    return lineOfOrder;
        //}

        //public SaleOrderDetail UpdateSaleOrderLine(SaleOrderDetail lineOfOrder)
        //{
        //    _context.Update(lineOfOrder);
        //    _context.SaveChanges();
        //    return lineOfOrder;
        //}

        //public void DeleteSaleOrderLine(int id)
        //{
        //    var solToDelete = _context.SaleOrdersDetails.SingleOrDefault(p => p.Id == id);

        //    if (solToDelete != null)
        //    {
        //        _context.SaleOrdersDetails.Remove(solToDelete);
        //        _context.SaveChanges();
        //    }
        //}
    }
}
