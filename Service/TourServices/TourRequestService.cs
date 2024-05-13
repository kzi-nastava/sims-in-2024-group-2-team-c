using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
            
            //TourRequest tourRequest = new TourRequest(false,startDate,endDate,location.Id,3, peopleinfoIds, language.Name,peopleInfos.Count(),description,LoggedInUser.Id);
            //Save(tourRequest);

            //touristService.UpdateTourRequests(tourRequest.Id);

        }

        public List<TouristRequestDTO> GetTouristRequests() { 
        
            List<TourRequest> requests = iTourRequestRepository.GetAll();

            List<TourRequest> touristsRequests = requests.Where(request => request.TouristId == LoggedInUser.Id).ToList();

            List<TouristRequestDTO> requestDtos = new List<TouristRequestDTO>();

            int number = 0;

            foreach (TourRequest request in touristsRequests) {

                TouristRequestDTO touristRequestDTO;
                if (request.Status == TourRequestStatus.Accepted)
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
        public List<string> GetAllLocations()
        {
            List<string> locations = new List<string>();
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            foreach (TourRequestDTO request in requests)
            {
                if(!locations.Contains(request.Location))
                    locations.Add(request.Location);
            }
            return locations;
        }
        public List<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            foreach (TourRequestDTO request in requests)
            {
                if (!languages.Contains(request.Language))
                    languages.Add(request.Language);
            }
            return languages;
        }
       /* public string GetMostPopularLocation() 
        {
            
        }*/
       public List<StatisticTourRequestDTO> GetTourRequestStatistics()
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            List<StatisticTourRequestDTO> dtos = new List<StatisticTourRequestDTO>();
            foreach(TourRequestDTO request in requests)
            {
                StatisticTourRequestDTO statistic = new StatisticTourRequestDTO 
                {
                    Location = request.Location,
                    Language = request.Language,
                    Year = request.StartDate.Year,
                    Month = request.StartDate.Month,
                    MonthNumberOfTours = CalculateNumberOfTourRequestByMonth(request.StartDate.Month),
                    YearNumberOfTours = CalculateNumberOfTourRequestByYear(request.StartDate.Year)
                };
                dtos.Add(statistic);
            }
            return dtos;
        }
        public List<StatisticTourRequestDTO> GetTourRequestStatisticsByYear(int SelectedYear)
        {
            List<StatisticTourRequestDTO> requests = GetTourRequestStatistics();
            List<StatisticTourRequestDTO> founded = new List<StatisticTourRequestDTO>();
            foreach (StatisticTourRequestDTO request in requests)
            {
                if (request.Year == SelectedYear)
                {
                    //request.YearNumberOfTours = CalculateNumberOfTourRequestByLocation(request.Location);
                    founded.Add(request);
                }

            }
            return founded;
        }
        public List<StatisticTourRequestDTO> GetTourRequestStatisticsByLocation(string SelectedLocation)
        {
            List<StatisticTourRequestDTO> requests = GetTourRequestStatistics();
            List<StatisticTourRequestDTO> founded = new List<StatisticTourRequestDTO>();
            foreach (StatisticTourRequestDTO request in requests)
            {
                if(request.Location == SelectedLocation)
                {
                    request.YearNumberOfTours = CalculateNumberOfTourRequestByLocation(request.Location);
                    founded.Add(request);
                }

            }
            return founded;
        }
        public List<StatisticTourRequestDTO> GetTourRequestStatisticsByLanguage(string SelectedLanguage)
        {
            List<StatisticTourRequestDTO> requests = GetTourRequestStatistics();
            List<StatisticTourRequestDTO> founded = new List<StatisticTourRequestDTO>();
            foreach (StatisticTourRequestDTO request in requests)
            {
                if (request.Language == SelectedLanguage)
                {
                    request.YearNumberOfTours = CalculateNumberOfTourRequestByLanguage(request.Language);
                    founded.Add(request);
                }

            }
            return founded;
        }
        public int CalculateNumberOfTourRequestByLocation(string SelectedLocation)
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            int count = 0;
            foreach (TourRequestDTO request in requests)
            {
                if (request.Location == SelectedLocation)
                {
                    count++;
                }
            }
            return count;
        }
        public int CalculateNumberOfTourRequestByLanguage(string SelectedLanguage)
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            int count = 0;
            foreach (TourRequestDTO request in requests)
            {
                if (request.Language == SelectedLanguage)
                {
                    count++;
                }
            }
            return count;
        }

        public int CalculateNumberOfTourRequestByMonth(int Month)
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            int count = 0;
            foreach (TourRequestDTO request in requests)
            {
                if(request.StartDate.Month == Month)
                {
                    count++;
                }
            }
            return count;
        }
        public int CalculateNumberOfTourRequestByYear(int Year)
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            int count = 0;
            foreach (TourRequestDTO request in requests)
            {
                if (request.StartDate.Year == Year)
                {
                    count++;
                }
            }
            return count;
        }

        public SelectedTourRequestDTO GetTourRequest(TouristRequestDTO touristRequest) {
        
            
            TourRequest request = GetById(touristRequest.TourRequestId);
            Location location = locationService.Get(request.LocationId);
            string CityAndCountry = location.City + ", " + location.Country;
            string activity;
            if (request.Status == TourRequestStatus.Accepted) {
                activity = "ACCEPTED";
            }
            else
            {
                activity = "INVALID";
            }

            SelectedTourRequestDTO selectedRequest = new SelectedTourRequestDTO(touristRequest.Number,request.StartDate,request.EndDate,request.Description, CityAndCountry,request.Language,activity);
            return selectedRequest;
        }

        public List<TourRequest> GetByTourist(int id)
        {
            List<TourRequest> requests = GetAll();
            List<TourRequest> touristsRequests = requests.Where(request => request.TouristId == id).ToList();

            return touristsRequests;

        }


        public TourRequest Update(TourRequest tourRequest)
        {
             return iTourRequestRepository.Update(tourRequest);

        }

        public List<TourRequestDTO> GetAllTourRequestDTOs()
        {
            List<TourRequest> requests = GetAll();
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach(TourRequest tr in requests)
            {
                    TourRequestDTO dto = new TourRequestDTO
                    {
                    Id = tr.Id,
                    Status = tr.Status,
                    StartDate = tr.StartDate,
                    EndDate = tr.EndDate,
                    Location = LoadLocation(tr.LocationId),
                    GuideId = tr.GuideId,
                    PeopleIds = tr.PeopleIds,
                    Language = tr.Language,
                    NumberOfPeople = tr.NumberOfPeople,
                    Description = tr.Description
                    };
                dtos.Add(dto);
            }
            return dtos;
        }
        public List<TourRequestDTO> GetOnHoldRequests()
        {
            List<TourRequestDTO> requests = GetAllTourRequestDTOs();
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach (TourRequestDTO tr in requests)
            {
                if (tr.Status == TourRequestStatus.OnHold)
                {
                    dtos.Add(tr);
                }
            }
            return dtos;
        }
        public string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }
        public void AcceptRequest(TourRequestDTO request)
        {
            TourRequest found = GetById(request.Id);
            if (found != null)
            {
                found.Status = TourRequestStatus.Accepted; //zahtev prihvacen
                found = Update(found);
                //return true;
            }
           // return false;
        }
        public List<TourRequestDTO> FilterRequestsByDate(DateTime SelectedStartDate, DateTime SelectedEndDate)
        {
            List<TourRequestDTO> filteredTourRequests = GetAllTourRequestDTOs();
            filteredTourRequests = filteredTourRequests.Where(t => t.StartDate.Date >= SelectedStartDate && t.EndDate.Date <= SelectedEndDate).ToList();
            return filteredTourRequests;
        }

        public List<TourRequestDTO> FilterRequestsByLocation(string SelectedLocation)
        {
            List<TourRequestDTO> filteredTourRequests = GetAllTourRequestDTOs();
            filteredTourRequests = filteredTourRequests.Where(t => t.Location == SelectedLocation).ToList();
            return filteredTourRequests;
        }

        public List<TourRequestDTO> FilterRequestsByNumberOfPeople(int SelectedNumberOfTourists)
        {
            List<TourRequestDTO> filteredTourRequests = GetAllTourRequestDTOs();
            filteredTourRequests = filteredTourRequests.Where(t => t.NumberOfPeople == SelectedNumberOfTourists).ToList();
            return filteredTourRequests;
        }

        public List<TourRequestDTO> FilterRequestsByLanguage(string SelectedLanguage)
        {
            List<TourRequestDTO> filteredTourRequests = GetAllTourRequestDTOs();
            filteredTourRequests = filteredTourRequests.Where(t => t.Language == SelectedLanguage).ToList();
            return filteredTourRequests;
        }


    }
}
