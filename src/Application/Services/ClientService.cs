using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        public void Add(Client client)
        {
            _repository.Add(client);
        }
    }
    
}
