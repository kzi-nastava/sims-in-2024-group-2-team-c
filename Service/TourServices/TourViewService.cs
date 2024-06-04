using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourViewService
    {
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        private LocationService locationService;
        public TourViewService() 
        {
            tourInstanceService = new TourInstanceService();
            tourService = new TourService();
            locationService = new LocationService();
        }
        private string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }

        public List<TourViewDTO> GetDTOs()
        {
            List<TourViewDTO> founded = new List<TourViewDTO>();
            List<TourInstance> instances = tourInstanceService.GetAll();
            foreach (TourInstance instance in instances)
            {
                Tour tour = tourService.GetById(instance.IdTour);
                TourViewDTO dto = new TourViewDTO
                {
                    Id = instance.Id,
                    Name = tour.Name,
                    Language = tour.Language,
                    Location = LoadLocation(tour.LocationId),
                    Description = tour.Description,
                    Duration = tour.Duration,
                    Date = instance.Date
                };
                founded.Add(dto);
            }
            return founded;
        }
    }
}
