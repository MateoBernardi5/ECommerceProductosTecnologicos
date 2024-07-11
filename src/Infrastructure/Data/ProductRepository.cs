using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Product? Get(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }


    }
}
