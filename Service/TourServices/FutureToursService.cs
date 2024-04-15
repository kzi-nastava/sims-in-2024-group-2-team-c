using BookingApp.DTO;
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
    internal class FutureToursService
    {
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        private TourVoucherService tourVoucherService;

        public FutureToursService()
        {
            tourInstanceService = new(new TourInstanceRepository());
            tourService = new(new TourRepository());
            tourVoucherService = new(new TourVoucherRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
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
                    Location = tour.ViewLocation,
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
        public void DeliverVoucherToTourists(int TourInstanceId)
        {
            TourInstance instance = tourInstanceService.GetById(TourInstanceId);
            Tour tour = tourService.GetById(instance.IdTour);
            List<int> tourists = tourService.FindPresentTourists(tour);
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
