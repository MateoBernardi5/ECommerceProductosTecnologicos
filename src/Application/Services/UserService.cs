using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User? Get(string name)
        {
            return _repository.Get(name);
        }

        public User? Get(int id)
        {
            return _repository.Get(id);
        }
        public void DeleteUser(int id)
        {
            var userToDelete = _repository.Get(id);
            if (userToDelete != null)
            {
                _repository.Delete(userToDelete);
            }
        }
    }
}
