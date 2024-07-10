using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService 
    {
        private readonly IClientRepository _repository;
        public ClientService(IClientRepository repository)
        {
            _repository = repository;   
        }

        public Client? Get(string name)
        {
            return _repository.Get(name);
        }
        public List<Client> GetClients()
        {
            return _repository.Get().ToList();
        }

        public int AddClient(ClientCreateRequest request)
        {
            var client = new Client()
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                UserType = "Client",
                Address = request.Address,
            };
            return _repository.Add(client).Id;
        }
    }
}