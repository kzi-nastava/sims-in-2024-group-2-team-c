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

        public TourRequestService()
        {
            iTourRequestRepository = Injectorr.CreateInstance<ITourRequestRepository>();
        }
        public void Save(TourRequest tourRequest)
        {
            iTourRequestRepository.Save(tourRequest);
        }
        public void Delete(TourRequest tourRequest) { iTourRequestRepository.Delete(tourRequest); }

        public List<TourRequest> GetAll() { return iTourRequestRepository.GetAll(); }
        public TourRequest GetById(int id) { return iTourRequestRepository.GetById(id); }
    }
}
