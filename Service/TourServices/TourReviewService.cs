using BookingApp.Interfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourReviewService
    {
        private readonly ITourReviewsRepository iTourRepository;
        //private readonly ITourInstanceRepository iTourInstanceRepository;
        //private TourInstanceService tourInstanceService;
        
        public TourReviewService()
        {
            //tourInstanceService = new(new TourInstanceRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }
    }
}
