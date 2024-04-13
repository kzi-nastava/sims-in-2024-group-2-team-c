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
    internal class TourInstanceService
    {
        private readonly ITourInstanceRepository iTourInstanceRepository;
        public TourInstanceService(ITourInstanceRepository iTourInstanceRepository)
        {
            this.iTourInstanceRepository = iTourInstanceRepository;
        }
        public List<TourInstance> GetAll() { return iTourInstanceRepository.GetAll(); }
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
    }
}
