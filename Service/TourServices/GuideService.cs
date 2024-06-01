using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class GuideService
    {
        public IGuideRepository GuideRepository { get; set; }
        private TourInstanceService tourInstanceService;
        public GuideService() 
        {
            GuideRepository = Injectorr.CreateInstance<IGuideRepository>();
            tourInstanceService = new TourInstanceService();
        }

        public Guide GetById(int id)
        {
            return GuideRepository.GetById(id);
        }
        public List<TourInstance> getInstancesById(int id)
        {
            Guide guide = GetById(id);
            List<int> instanceIds = guide.TourInstancesIds;
            List<TourInstance> instances = new List<TourInstance>();
            foreach(int instanceId in instanceIds)
            {
                TourInstance instance = tourInstanceService.GetById(instanceId);
                instances.Add(instance);
            }
            return instances;
        }
    }
}
