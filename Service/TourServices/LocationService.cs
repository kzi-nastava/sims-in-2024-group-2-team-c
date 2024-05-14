﻿using BookingApp.Injector;
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

    }
}
