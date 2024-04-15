using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITouristsRepository
    {
        List<Tourist> GetAll();
        Tourist Save(Tourist tour);
        int NextId();
        void Delete(Tourist tour);
        Tourist Update(Tourist tour);
        Tourist GetById(int id);
    }
}
