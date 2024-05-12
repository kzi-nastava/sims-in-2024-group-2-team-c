using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TouristStatisticsService
    {

        private readonly LocationService _locationService;

        private readonly TourRequestService _requestService;
        
        public TouristStatisticsService() {
            _requestService = new TourRequestService(); 
           _locationService = new LocationService();
        }


        public Dictionary<string, int> GroupByLocation(List<TourRequest> requests)
        {
            
            Dictionary<string, int> requestsByCountry = new Dictionary<string, int>();

            foreach (var tourRequest in requests)
            {
                string country = GetCountryName(tourRequest.LocationId);
                if (!requestsByCountry.ContainsKey(country))
                {
                    requestsByCountry[country] = 0;
                }
                requestsByCountry[country]++;
            }

            return requestsByCountry;
        }


        private  string GetCountryName(int locationId)
        {

            List<Location> locations = _locationService.GetAll();

            foreach (var location in locations)
            {
                if (location.Id == locationId)
                {
                    return location.Country;
                }
            }
            return null; // Handle if location ID is not found
        }


        public List<int> GetRequestYears()
        {

           List<TourRequest> requests=  _requestService.GetByTourist(LoggedInUser.Id);

            List<int> years = new List<int>();

            foreach (var request in requests)
            {

                int year = request.CreationDate.Year;

                if (!years.Contains(year) && request.Status != TourRequestStatus.OnHold)
                {
                    // Add the item to the list
                    years.Add(year);
                }

            }

            return years;


        }

        public int CountAcceptedRequests(int year)
        {

            int counter = 0;

            List<TourRequest> requests = _requestService.GetByTourist(LoggedInUser.Id);

           

            foreach (var request in requests)
            {
                

                if(year == 0)
                {
                    if (request.Status == TourRequestStatus.Accepted) { counter++; }
                }
                else
                {
                    if (request.Status == TourRequestStatus.Accepted && request.CreationDate.Year == year) { counter++; }
                }

                
            }
            return counter;
        }

        public int CountInvalidRequests(int year)
        {

            int counter = 0;

            List<TourRequest> requests = _requestService.GetByTourist(LoggedInUser.Id);


            foreach (var request in requests)
            {

                if (year == 0)
                {
                    if (request.Status == TourRequestStatus.Invalid) { counter++; }
                }
                else
                {
                    if (request.Status == TourRequestStatus.Invalid && request.CreationDate.Year == year) { counter++; }
                }
            }
            return counter;
        }

    }
}
