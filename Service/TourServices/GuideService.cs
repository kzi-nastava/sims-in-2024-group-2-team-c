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
    public class GuideService
    {
        public IGuideRepository GuideRepository { get; set; }
        private TourInstanceService tourInstanceService;
        private readonly IUserRepository userRepository;
        public GuideService() 
        {
            GuideRepository = Injectorr.CreateInstance<IGuideRepository>();
            tourInstanceService = new TourInstanceService();
            userRepository = new UserRepository();
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
        public List<DateTime> GetUnAvailableTimeSlots(DateTime startDate, DateTime endDate, int guideId)
        {
            //List<DateTime> availableSlots = new List<DateTime>();
            List<DateTime> unavailableSlots = new List<DateTime>();
            //Guide guide = GetById(guideId);
            List<TourInstance> instances = tourInstanceService.GetAll();
            User guide = userRepository.GetUserById(guideId);
            //foreach(int id in guide.TourInstancesIds)
            foreach(TourInstance instance in instances)
            {
                //TourInstance instance = tourInstanceService.GetById(id);
                unavailableSlots.Add(instance.Date);
            }
            return unavailableSlots;
        }
    }
}
