using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IAccommodationRateRepository
    {
        bool HasUserRatedAccommodation(int userId, int Id);
        public void Save(AccommodationRate accommodationRate);
        public List<AccommodationRate> GetAll();
        public string GetFilePath();
        void Update(AccommodationRate accommodationRate);
        public AccommodationRate GetById(int id);
    }

}



