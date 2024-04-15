using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourRepository
    {
        
            List<Tour> GetAll();
            Tour Save(Tour tour);
            int NextId();
            void Delete(Tour tour);
            Tour Update(Tour tour);
            Tour GetById(int id);
            List<Tour> GetToursByLocationId(int locationId);

        public string GetTourNameById(int tourId);
    }
}
