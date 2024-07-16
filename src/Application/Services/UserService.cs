using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;

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

        //public string Authenticate(CredentialsDtoRequest credentials)
        //{
        //    return _authenticateService.Authenticate(credentials);
        //}
    }
}