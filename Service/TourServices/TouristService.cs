using BookingApp.DTO;
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
    public class TouristService
    {
        private readonly ITouristsRepository iTouristRepository;
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        

        public TouristService()
        {
            iTouristRepository = Injectorr.CreateInstance<ITouristsRepository>();
            tourInstanceService = new TourInstanceService();
            tourService = new TourService();
            //tourInstanceService = new(new TourInstanceRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }
        public Tourist GetById(int id)
        {
            return iTouristRepository.GetById(id);
        }

        public bool GetActivity(int id) {
            Tourist tourist = iTouristRepository.GetById(id);
            return tourist.Active;
        }

        public void Activate(int id)
        {
            Tourist tourist = iTouristRepository.GetById(id);
            tourist.Active = true;
            iTouristRepository.Update(tourist);
        }

        public string GetFirstName(int id)
        {
            Tourist tourist = iTouristRepository.GetById(id);
            return tourist.FirstName;
        }

        public string GetLastName(int id)
        {
            Tourist tourist = iTouristRepository.GetById(id);
            return tourist.LastName;
        }

        public void UpdateTourRequests(int tourRequestId)
        {
            Tourist tourist = GetById(LoggedInUser.Id);
            tourist.TourRequestIds.Add(tourRequestId);
            iTouristRepository.Update(tourist);

        }

       
    }
}
