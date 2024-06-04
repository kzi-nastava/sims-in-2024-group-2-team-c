using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service.TourServices
{
    public class SearchTourService
    {

        private readonly TourService _tourService;
        private readonly LocationService locationService;
        public SearchTourService() {
            _tourService = new TourService();
            locationService = new LocationService();
        }


        public List<HomeTourDTO> GetFilteredTours(string location,string language,int? duration)
        {

            List<Tour> tours = _tourService.GetAll();
            List<HomeTourDTO> homeTours = new List<HomeTourDTO>();

            tours = FilterByLocation(location, tours);
            tours = FilterByDuration(duration, tours);
            tours = FilterByLanguage(language, tours);


            foreach (Tour tour in tours)
            {

                Location singleLocation = locationService.GetById(tour.LocationId);
                string locationString = $"{singleLocation.Country}";



                HomeTourDTO homeTour = new HomeTourDTO(tour.Id, tour.Name, locationString, tour.Images);


                homeTours.Add(homeTour);

            }


            return homeTours;

        }


        private static List<Tour> FilterByLanguage(string language, List<Tour> tours)
        {
            if (!string.IsNullOrEmpty(language))
            {
                tours = tours.Where(t => t.Language.ToLower().Contains(language.ToLower())).ToList();
            }

            return tours;
        }

        private static List<Tour> FilterByDuration(int? duration, List<Tour> tours)
        {
            if (duration != 0)
            {
                    tours = tours.Where(t => t.Duration == duration).ToList();
            }

            return tours;
        }

        private List<Tour> FilterByLocation(string location, List<Tour> tours)
        {
            int locationId = 0;
            if (!string.IsNullOrEmpty(location))
            {
                locationId = locationService.GetIdByCityorCoutry(location);

                tours = _tourService.GetToursByLocationId(locationId);

            }

            return tours;
        }



    }
}
