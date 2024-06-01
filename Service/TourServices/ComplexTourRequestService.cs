using BookingApp.Injector;
using BookingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class ComplexTourRequestService
    {

        private readonly IComplexTourRequestRepository complexTourRequestRepository;
        

        public ComplexTourRequestService() {
            complexTourRequestRepository = Injectorr.CreateInstance<IComplexTourRequestRepository>();
        }





    }
}
