using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);
        public User GetUserById(int id);
    }
}
