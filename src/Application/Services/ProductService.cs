﻿using Application.Interfaces;
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
    public class ProductService 
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

       
        public Product? Get(string name)
        {
            return _repository.Get(name);
        }

        public Product? Get(int id) 
        { 
            return _repository.Get(id);
        }

        public int AddProduct(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            };
            return _repository.Add(product).Id;
        }
        

        public void DeleteProduct(int id)
        {
            var productToDelete = _repository.Get(id);
            if (productToDelete != null)
            {
                _repository.Delete(productToDelete);
            }
        }

        //public Product UpdateProduct(ProductUpdateRequest request)
        //{
        //    var product = _repository.Get(request.Id);
        //    if (product != null)
        //    {
                
        //        product.Price = request.Price;
        //        product.Stock = request.Stock;

        //        _repository.Update(product);
        //    }
        //    return product;
        //}
    }
}

