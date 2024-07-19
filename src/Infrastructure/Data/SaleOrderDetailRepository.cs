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

        public SaleOrderDetail? GetById(int id)
        {
            return _context.SaleOrderDetails
                .Include(sol => sol.Product)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .SingleOrDefault(x => x.Id == id);
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

        public List<SaleOrderDetail> GetAllByClient(int clientId)
        {
            return _context.SaleOrderDetails
                .Include(sol => sol.Product)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .Where(sol => sol.SaleOrder.ClientId == clientId)
                .ToList();
        }

        //public bool ProductExists(int productId)
        //{
        //    return _context.Products.Any(p => p.Id == productId);
        //}

        public bool SaleOrderExists(int saleOrderId)
        {
            return _context.SaleOrders.Any(s => s.Id == saleOrderId);
        }
        public Product? GetProduct(int productId)
        {
            return _context.Products.Find(productId);
        }
    }
}
