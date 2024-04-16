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

namespace BookingApp.Service.TourServices
{
    public class TourInstanceService
    {
        private readonly ITourInstanceRepository iTourInstanceRepository;
        public TourInstanceService()
        {
            iTourInstanceRepository = Injectorr.CreateInstance<ITourInstanceRepository>();
        }
        public TourInstance GetById(int id) { return iTourInstanceRepository.GetById(id); }
        public List<TourInstance> GetAll() { return iTourInstanceRepository.GetAll(); }
        public void Delete(TourInstance tourInstance) { iTourInstanceRepository.Delete(tourInstance); }
        public List<TourInstance> GetFutureInstance()
        {
            List<TourInstance> instances = iTourInstanceRepository.GetAll();
            List <TourInstance> founded = new List<TourInstance>();
            foreach (TourInstance instance in instances)
            {
                if(instance.Date > DateTime.Now.AddHours(48))
                {
                    founded.Add(instance);
                }
            }
            return founded;
        }
        public List<TourInstance> GetEndedInstances()
        {
            List<TourInstance> instances = iTourInstanceRepository.GetAll();
            List<TourInstance> founded = new List<TourInstance>();
            foreach (TourInstance instance in instances)
            {
                if (instance.Ended == true)
                {
                    founded.Add(instance);
                }
            }
            return founded;
        }
    }
}
