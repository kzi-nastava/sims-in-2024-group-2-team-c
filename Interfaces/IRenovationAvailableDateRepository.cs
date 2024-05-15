using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    internal interface IRenovationAvailableDateRepository
    {
        public List<RenovationAvailableDate> GetAll();
        public void Save(RenovationAvailableDate renovation);
        public void Delete(int id);
        public int NextId();
        public void Update(RenovationAvailableDate renovation);
    }
}
