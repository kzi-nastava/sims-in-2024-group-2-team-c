using BookingApp.Interfaces;
using BookingApp.Repository;
using BookingApp.Service.TourServices;

using BookingApp.Service.AccommodationServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Injector
{
    public class Injectorr
    {


        
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            //{ typeof(IUserRepository), new UserRepository() },
            { typeof(ITourReccommendationsRepository), new TourReccommendationsRepository() },
            { typeof(ITourRequestRepository), new TourRequestRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(ITourReviewsRepository), new TourReviewsRepository() },
            { typeof(ITourVoucherRepository), new TourVoucherRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            // { typeof(ITourService), new TourService() }

            { typeof(ITourRequestNotificationRepository), new TourRequestNotificationRepository() },
            // { typeof(IAccommodationService), new AccommodationService() }

            { typeof(ILocationRepository), new LocationRepository() },
           // { typeof(ITourRepository), new TourRepository() },
            // { typeof(ITourService), new TourService() }


            {typeof(ITourInstanceRepository), new TourInstanceRepository() },
            // {typeof(FollowTourService), new FollowTourService() },

           // {typeof(ITourReviewsRepository), new TourReviewsRepository() },
            {typeof(ITouristNotificationRepository), new TouristNotificationRepository() },
            //{typeof(ITourRepository), new TourRepository() },
            //{typeof(ITourInstanceRepository), new TourInstanceRepository() },
            {typeof(ITouristsRepository), new TouristRepository() },
            {typeof(IPeopleInfoRepository),new PeopleInfoRepository()},

            {typeof(IKeyPointRepository),new KeyPointRepository()},


            
            // { typeof(ITourService), new TourService() }

            //{ typeof(IReservationRepository), new ReservationRepository() }
            //{ typeof(IGuestReservationService), new GuestReservationService() }

              { typeof(ILanguageRepository), new LanguageRepository() },
            { typeof(IAccommodationRateRepository),new AccommodationRateRepository()},

            { typeof(IReservationDelayRepository),new ReservationDelayRepository()},

            { typeof(IGuestRatingRepository),new GuestRatingRepository()},

            { typeof(IGuestRepository),new GuestRepository()},

            { typeof(IGuestNotificationRepository),new GuestNotificationRepository()},

            { typeof(IOwnerNotificationRepository),new OwnerNotificationRepository()},


            { typeof(IRenovationRepository), new RenovationRepository()},

            { typeof(IRenovationAvailableDateRepository), new RenovationAvailableDateRepository() },

            { typeof(IGuestReservationRepository), new GuestReservationRepository() },


            { typeof(IForumRepository), new ForumRepository() }
            

        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
