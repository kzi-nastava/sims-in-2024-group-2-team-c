using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.Service.TourServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository iTourRequestRepository;
        private readonly PeopleInfoService peopleInfoService;
        private readonly TouristService touristService;
        private readonly LocationService locationService;
        

        public TourRequestService()
        {
            iTourRequestRepository = Injectorr.CreateInstance<ITourRequestRepository>();
            peopleInfoService = new PeopleInfoService();
            touristService = new TouristService();
            locationService = new LocationService();


        }
        public void Save(TourRequest tourRequest)
        {
            iTourRequestRepository.Save(tourRequest);
        }
        public void Delete(TourRequest tourRequest) { iTourRequestRepository.Delete(tourRequest); }

        public List<TourRequest> GetAll() { return iTourRequestRepository.GetAll(); }
        public TourRequest GetById(int id) { return iTourRequestRepository.GetById(id); }



        public void CreateTourRequest(Location location,string description,Language language,DateTime startDate, DateTime endDate,List<PeopleInfo> peopleInfos)
        {

            List<int> peopleinfoIds = peopleInfoService.SavePeopleInfoList(peopleInfos);
            
            TourRequest tourRequest = new TourRequest(false,startDate,endDate,location.Id,3, peopleinfoIds, language.Name,peopleInfos.Count(),description,LoggedInUser.Id);
            Save(tourRequest);

            touristService.UpdateTourRequests(tourRequest.Id);

        }

        public List<TouristRequestDTO> GetTouristRequests() { 
        
            List<TourRequest> requests = iTourRequestRepository.GetAll();

            List<TourRequest> touristsRequests = requests.Where(request => request.TouristId == LoggedInUser.Id).ToList();

            List<TouristRequestDTO> requestDtos = new List<TouristRequestDTO>();

            int number = 0;

            foreach (var request in touristsRequests) {

                TouristRequestDTO touristRequestDTO;
                if (request.Status)
                {
                    touristRequestDTO = new TouristRequestDTO("ACCEPTED", ++number, request.Id);
                }
                else
                {
                    touristRequestDTO = new TouristRequestDTO("INVALID", ++number, request.Id);
                }
                requestDtos.Add(touristRequestDTO);


            }


            return requestDtos;
        }




        public SelectedTourRequestDTO GetTourRequest(TouristRequestDTO touristRequest) {
        
            
            TourRequest request = GetById(touristRequest.TourRequestId);
            Location location = locationService.Get(request.LocationId);
            string CityAndCountry = location.City + ", " + location.Country;
            string activity;
            if (request.Status) {
                activity = "ACCEPTED";
            }
            else
            {
                activity = "INVALID";
            }

            SelectedTourRequestDTO selectedRequest = new SelectedTourRequestDTO(touristRequest.Number,request.StartDate,request.EndDate,request.Description, CityAndCountry,request.Language,activity);
            return selectedRequest;
        }


       

    }
}
