﻿using BookingApp.DTO;
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
        private LocationService locationService;

        public EndedToursService()
        {
            tourInstanceService = new(new TourInstanceRepository());
            tourService = new(new TourRepository());
            locationService = new(new LocationRepository());
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
                    MoreThan50 = tourService.CalculateNumberOfTouristsMore50(tour)
                };
                founded.Add(dto);
            }
            return founded;
        }
        private string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }
        public float CalculateAttendancePercentage(TourStatisticDTO tour)
        {
            float perc = 0;
            perc = tour.PresentTourists / tour.MaxTourists;
            return perc;
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
    }
}
