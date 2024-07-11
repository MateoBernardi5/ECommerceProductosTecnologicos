﻿using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public List<Admin> GetAllAdmins()
        {
            return _repository.Get();
        }

        public Admin? Get(int id)
        {
            return _repository.Get(id);
        }

        public Admin? Get(string name)
        {
            return _repository.Get(name);
        }

        public int AddAdmin(AdminCreateRequest request)
        {
            var admin = new Admin()
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                UserType = "Admin"
            };
            return _repository.Add(admin).Id;
        }

        public void DeleteAdmin(int id)
        {
            var adminToDelete = _repository.Get(id);
            if (adminToDelete != null )
            {
                _repository.Delete(adminToDelete);
            }
        }
    }
}