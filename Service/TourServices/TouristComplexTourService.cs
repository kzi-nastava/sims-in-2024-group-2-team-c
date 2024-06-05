using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TouristComplexTourService
    {
        private readonly ComplexTourRequestService _complexRequestService;
        private readonly RequestForComplexTourService _requestForComplexTourService;
        private readonly LocationService _locationService;


        public TouristComplexTourService() {
            _complexRequestService = new ComplexTourRequestService();
            _requestForComplexTourService = new RequestForComplexTourService();
            _locationService = new LocationService();
        }

        public List<ComplexTouristRequestDTO> GetPartsByTourId(int id)
        {

            ComplexTourRequest complexRequest = _complexRequestService.GetById(id);
            List<int> parts = complexRequest.TourRequestIds;

            List<TourRequest> tourRequests = _requestForComplexTourService.GetAll();

            List<TourRequest> filteredRequests = tourRequests.Where(request => parts.Contains(request.Id)).ToList();
            int number = 0;
            List<ComplexTouristRequestDTO> loadedRequests = new List<ComplexTouristRequestDTO>();
            foreach (TourRequest tourRequest in filteredRequests)
            {
                ComplexTouristRequestDTO complexTouristRequestDTO;
                if (tourRequest.Status == TourRequestStatus.Accepted)
                {
                    complexTouristRequestDTO = new ComplexTouristRequestDTO("ACCEPTED", ++number, tourRequest.Id,DateTime.Now);
                }
                else if (tourRequest.Status == TourRequestStatus.Invalid)
                {
                    complexTouristRequestDTO = new ComplexTouristRequestDTO("INVALID", ++number, tourRequest.Id, DateTime.Now);
                }
                else
                {
                    complexTouristRequestDTO = new ComplexTouristRequestDTO("ON HOLD", ++number, tourRequest.Id, DateTime.Now);
                }
                loadedRequests.Add(complexTouristRequestDTO);

            }
            return loadedRequests;

        }


        public TourRequestDTO LoadRequest(int id) {

            TourRequest request = _requestForComplexTourService.GetById(id);

            TourRequestDTO tourRequestDTO = new TourRequestDTO
            {
                Id = request.Id,
                Status = request.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = LoadLocation(request.LocationId),
                GuideId = request.GuideId,
                PeopleIds = request.PeopleIds,
                Language = request.Language,
                NumberOfPeople = request.NumberOfPeople,
                Description = request.Description
            }; ;

            return tourRequestDTO;

        }

        public string LoadLocation(int locationId)
        {
            Location location = _locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }


    }
}
