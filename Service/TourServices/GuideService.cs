using BookingApp.Injector;
using BookingApp.Interfaces;
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
        public GuideService() 
        {
            GuideRepository = Injectorr.CreateInstance<IGuideRepository>();
        }

    }
}
