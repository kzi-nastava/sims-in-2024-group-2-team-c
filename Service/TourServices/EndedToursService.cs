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
        private KeyPointService keyPointService;
        private LocationService locationService;

        public EndedToursService()
        {
            tourInstanceService = new TourInstanceService();
            tourService = new();
            locationService = new LocationService();
            keyPointService = new KeyPointService();
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
                    Location = LoadLocation(tour.LocationId),
                    Language = tour.Language,
                    Duration = tour.Duration,
                    Date = instance.Date,
                    MaxTourists = instance.MaxTourists,
                    ReservedTourists = instance.ReservedTourists,
                    PresentTourists = tourService.FindPresentTouristsCount(instance.IdTour),
                    LessThan18 = tourService.CalculateNumberOfTouristsUnder18(tour),
                    Between18And50 = tourService.CalculateNumberOfTourists18And50(tour),
                    MoreThan50 = tourService.CalculateNumberOfTouristsMore50(tour),
                    Attendence = tourService.CalculateAttendacePercentage(instance),
                    KeyPoint = LoadKeyPoints(tour.KeyPointIds)
                };
                founded.Add(dto);
            }
            return founded;
        }
        public string LoadKeyPoints(List<int> Ids)
        {
            //string keyPoints = string.Empty;
            List<string> keyPointName = new List<string>();
            foreach(int id in Ids)
            {
                KeyPoint kp = keyPointService.GetById(id);
                keyPointName.Add(kp.Name);
                
            }
            return string.Join(",", keyPointName);
        }
        public string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }
        public float CalculateAttendancePercentage(TourStatisticDTO tour)
        {
            return (float)tour.PresentTourists / tour.MaxTourists;

        }
        public List<float> FindAttendencePercentagesForTours()
        {
            List<TourStatisticDTO> endedTours = GetEndedTours();
            List<float> percentages = new List<float>();
            foreach (TourStatisticDTO tour in endedTours)
            {
                percentages.Add(CalculateAttendancePercentage(tour));
            }
            return percentages;
        }
        public List<float> FindAttendencePercentagesForToursByYear(int year)
        {
            List<TourStatisticDTO> endedTours = FindEndedToursByYear(year);
            List<float> percentages = new List<float>();
            foreach (TourStatisticDTO tour in endedTours)
            {
                percentages.Add(CalculateAttendancePercentage(tour));
            }
            return percentages;
        }
        public TourStatisticDTO FindMostVisitedTour()
        {
            List<TourStatisticDTO> endedTours = GetEndedTours();
            List<float> percentages = FindAttendencePercentagesForTours();
            TourStatisticDTO founded = new TourStatisticDTO();
            foreach (TourStatisticDTO tour in endedTours)
            {
                if (percentages.Max() == CalculateAttendancePercentage(tour))
                {
                    founded = tour;
                }
                    
            }
            return founded;
        }
        public TourStatisticDTO FindMostVisitedTourForYear(int year)
        {
            List<TourStatisticDTO> endedTours = FindEndedToursByYear(year);
            List<float> percentages = FindAttendencePercentagesForToursByYear(year);
            TourStatisticDTO founded = new TourStatisticDTO();
            foreach (TourStatisticDTO tour in endedTours)
            {
                if (percentages.Max() == CalculateAttendancePercentage(tour))
                {
                    founded = tour;
                }

            }
            return founded;

        }

        public List<TourStatisticDTO> FindEndedToursByYear(int year)
        {
            List<TourStatisticDTO> founded = new List<TourStatisticDTO>();
            List<TourInstance> instances = tourInstanceService.FindEndedToursInstancesByYear(year);
            foreach (TourInstance instance in instances)
            {
                Tour tour = tourService.GetById(instance.IdTour);
                TourStatisticDTO dto = new TourStatisticDTO
                {
                    TourInstanceId = instance.IdTour,
                    Name = tour.Name,
                    Description = tour.Description,
                    Location = LoadLocation(tour.LocationId),
                    Language = tour.Language,
                    Duration = tour.Duration,
                    Date = instance.Date,
                    MaxTourists = instance.MaxTourists,
                    ReservedTourists = instance.ReservedTourists,
                    PresentTourists = tourService.FindPresentTouristsCount(instance.IdTour),
                    LessThan18 = tourService.CalculateNumberOfTouristsUnder18(tour),
                    Between18And50 = tourService.CalculateNumberOfTourists18And50(tour),
                    MoreThan50 = tourService.CalculateNumberOfTouristsMore50(tour)
                };
                founded.Add(dto);
            }
            return founded;
        }
    }
}
