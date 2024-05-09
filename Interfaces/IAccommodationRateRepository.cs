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
        bool HasUserRatedAccommodation(int userId, string name);
        public void Save(AccommodationRate accommodationRate);

        public List<AccommodationRate> GetAll();
        public string GetFilePath();

    }

}



