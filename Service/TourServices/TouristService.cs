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
    public class TouristService
    {
        private readonly ITouristsRepository iTouristRepository;
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        

        public TouristService(ITouristsRepository itouristRepository)
        {
            iTouristRepository = itouristRepository;
            tourInstanceService = new(new TourInstanceRepository());
            tourService = new(new TourRepository());
            //tourInstanceService = new(new TourInstanceRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }
        public Tourist GetById(int id)
        {
            return iTouristRepository.GetById(id);
        }

    }
}
