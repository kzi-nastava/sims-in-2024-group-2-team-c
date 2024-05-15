using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    internal interface IRenovationRepository
    {
        public List<Renovation> GetAll();
        public void Save(Renovation renovation);
        public void Delete(int id);
        public int NextId();
    }
}
