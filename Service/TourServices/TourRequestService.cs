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

namespace BookingApp.Service.TourServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository iTourRequestRepository;
        private LocationService locationService;

        public TourRequestService()
        {
            iTourRequestRepository = Injectorr.CreateInstance<ITourRequestRepository>();
            locationService = new LocationService();
        }
        public void Save(TourRequest tourRequest)
        {
            iTourRequestRepository.Save(tourRequest);
        }
        public void Delete(TourRequest tourRequest) { iTourRequestRepository.Delete(tourRequest); }

        public List<TourRequest> GetAll() { return iTourRequestRepository.GetAll(); }
        public TourRequest GetById(int id) { return iTourRequestRepository.GetById(id); }
        public void Update(TourRequest tourRequest)
        {
             tourRequest = iTourRequestRepository.Update(tourRequest);

        }

        public List<TourRequestDTO> GetAllTourRequestDTOs()
        {
            List<TourRequest> requests = GetAll();
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach(TourRequest tr in requests)
            {
                if(tr.Status != true)
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
            }
            return dtos;
        }
        public string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }
        public bool AcceptRequest(TourRequestDTO request)
        {
            TourRequest found = GetById(request.Id);
            if (found != null)
            {
                found.Status = true; //zahtev prihvacen
                Update(found);
                return true;
            }
            return false;
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
