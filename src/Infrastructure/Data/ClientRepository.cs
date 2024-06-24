using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;
        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Client? Get(string name)
        {
            return _context.Clients.FirstOrDefault(x => x.Name == name);
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
    }
}
