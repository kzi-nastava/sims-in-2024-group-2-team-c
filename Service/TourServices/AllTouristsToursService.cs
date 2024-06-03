using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class AllTouristsToursService
    {

        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly LocationService _locationService;
        private readonly TouristService _touristService;
        private readonly TourReservationService _tourReservationService;


        public AllTouristsToursService() {


            _locationService = new LocationService();
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            _tourReservationService = new TourReservationService();
            _touristService = new TouristService();


        }


        public List<TouristPdfDTO> GetAllToursInYear()
        {

            //Tourist tourist = _touristService.GetById(LoggedInUser.Id);
            List<TourReservation> reservations = _tourReservationService.GetByMainTouristId(LoggedInUser.Id);

            List<int> tourInstanceIds = reservations.Select(r => r.TourInstanceId).ToList();

            List<TourInstance> allTourInstances = _tourInstanceService.GetAll();

            List<TourInstance> filteredTourInstances = allTourInstances.Where(t => tourInstanceIds.Contains(t.Id) && t.Date.Year == 2023).ToList();

            List<TouristPdfDTO> touristPdfDTOs = new List<TouristPdfDTO>();

            foreach(TourInstance t in filteredTourInstances)
            {

                Tour tour = _tourService.GetById(t.IdTour);

                Location location = _locationService.GetById(tour.LocationId);
                string locationString = $"{location.City}, {location.Country}";

                TouristPdfDTO tourPdf = new TouristPdfDTO(tour.Name, tour.Language, tour.Duration, locationString, tour.Description, tour.Images,t.Date);


                touristPdfDTOs.Add(tourPdf);

            }

            return touristPdfDTOs;

        }

       



    }
}
