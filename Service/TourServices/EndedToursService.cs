using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Service.TourServices
{
    public class EndedToursService
    {
        private TourInstanceService tourInstanceService;
        private TourService tourService;

        public EndedToursService()
        {
            tourInstanceService = new(new TourInstanceRepository());
            tourService = new(new TourRepository());
        }

        public List<TourStatisticDTO> GetEndedTours()
        {
            List<TourStatisticDTO> founded = new List<TourStatisticDTO>();
            List<TourInstance> instances = tourInstanceService.GetEndedInstances();
            foreach (TourInstance instance in instances)
            {
                Tour tour = tourService.GetById(instance.IdTour);
                TourStatisticDTO dto = new TourStatisticDTO
                {
                    TourInstanceId = instance.IdTour,
                    Name = tour.Name,
                    Description = tour.Description,
                    Location = tour.ViewLocation,
                    Language = tour.Language,
                    Duration = tour.Duration,
                    Date = instance.Date,
                    MaxTourists = instance.MaxTourists,
                    ReservedTourists = instance.ReservedTourists,
                    //PresentTourists = tourService.FindPresentTouristsCount(instance.IdTour)
            };
                founded.Add(dto);
            }
            return founded;
        }
    }
}
