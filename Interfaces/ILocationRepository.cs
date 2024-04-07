using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        int GetIdByCityorCountry(string searchTerm);
        Location Save(Location location);
        int NextId();
        void Delete(Location location);
        Location Update(Location location);
        Location FindLocation(string city, string country);
        Location GetById(int id);
        Location Get(int id);
        int GetIdByCityorCoutry(string searchString);
        Location GetLocationByCity(string cityName);

    }
}
