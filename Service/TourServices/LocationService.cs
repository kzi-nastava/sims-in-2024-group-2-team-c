using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class LocationService
    {
        private readonly ILocationRepository locationRepository;
        //private TourInstanceService tourInstanceService;
        //private TourService tourService;


        public LocationService()
        {
            locationRepository = Injectorr.CreateInstance<ILocationRepository>();
            //tourInstanceService = new TourInstanceService();
            //tourService = new TourService();
        }

        public List<Location> GetAll() { return locationRepository.GetAll(); }

        public Location GetById(int id)
        {
            return locationRepository.GetById(id);
        }


        public int GetIdByCityorCoutry(string searchString)
        {
            return locationRepository.GetIdByCityorCoutry(searchString);
        }

        public Location Get(int id)
        {
            return locationRepository.Get(id);
        }

        public Location FindLocation(string city, string country)
        {
            return locationRepository.FindLocation(city, country);
        }
        public void Save(Location location)
        {
            locationRepository.Save(location);
        }

        public int GetIdByCountry(string country)
        {
            List<Location> locations = locationRepository.GetAll();

            foreach (Location location in locations)
            {
                // Check if the country matches
                if (location.Country.ToLower().Trim() == country.ToLower().Trim())
                {
                    // Return the ID of the matching location
                    return location.Id;
                }
            }

            // If no matching location is found, return a default value or throw an exception
            // Depending on your requirements
            return -1;

        }

    }
}
