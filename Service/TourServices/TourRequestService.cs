using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository iTourRequestRepository;
        private readonly PeopleInfoService peopleInfoService;
        private readonly TouristService touristService;
        

        public TourRequestService()
        {
            iTourRequestRepository = Injectorr.CreateInstance<ITourRequestRepository>();
            peopleInfoService = new PeopleInfoService();
            touristService = new TouristService();


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


    }
}
