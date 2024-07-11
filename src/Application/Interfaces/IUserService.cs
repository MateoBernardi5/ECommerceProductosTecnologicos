using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User? Get(string name);
        User? Get(int id);
        void DeleteUser(int id);
    }
}
