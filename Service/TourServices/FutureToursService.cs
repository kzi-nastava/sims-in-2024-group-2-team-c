using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BookingApp.Service.TourServices
{
    internal class FutureToursService
    {
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        private LocationService locationService;
        private TourVoucherService tourVoucherService;

        public FutureToursService()
        {
            tourInstanceService = new TourInstanceService();
            tourService = new TourService();
            locationService = new LocationService();
            tourVoucherService = new TourVoucherService();
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }

        private string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }

        public List<FutureTourDTO> GetFutureTourDTOs()
        {
            List<FutureTourDTO> founded = new List<FutureTourDTO>();
            List<TourInstance> instances = tourInstanceService.GetFutureInstance();
            foreach(TourInstance instance in instances)
            {
                Tour tour = tourService.GetById(instance.IdTour);
                FutureTourDTO futureTour = new FutureTourDTO
                {
                    Id = instance.Id,
                    Name = tour.Name,
                    Language = tour.Language,
                    Location = LoadLocation(tour.LocationId),
                    Description = tour.Description,
                    Duration = tour.Duration,
                    Date = instance.Date
                };
                founded.Add(futureTour);
            }
            return founded;
        }
        public void CancelTour(int TourInstanceId) //id od klase FutureDTO je tour instance id 
        {
            TourInstance instance = tourInstanceService.GetById(TourInstanceId);
            //Za sve prijavljene turiste se salju vauceri!
            tourInstanceService.Delete(instance);
        }
       /* public void CancelToursByGuide(int guideId)
        {
            List<TourInstance> futureTours = tourInstanceService.GetFutureInstanceByGuide(guideId);
            foreach (TourInstance tour in futureTours)
            {
                CancelTour(tour.Id);
                DeliverVoucherToTourists(tour.Id);
            }
        }*/
        public void DeliverVoucherToTourists(int TourInstanceId)
        {
            TourInstance instance = tourInstanceService.GetById(TourInstanceId);
            Tour tour = tourService.GetById(instance.IdTour);
            List<int> tourists = tourService.FindPresentTourists(tour);
            if (tourists != null)
            {
                foreach(int id in tourists)
                {
                    TourVoucher voucher = new TourVoucher
                    {
                        TourId = tour.Id,
                        TouristId = id,
                        ExpirationDate = DateTime.Today.AddYears(1)
                    };
                    tourVoucherService.Send(voucher);
                }
            }
            
        }
    }
}
