using BookingApp.Interfaces;
using BookingApp.Repository;
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
            { typeof(IUserRepository), new UserRepository() },
            //{ typeof(IUserService), new UserService() },

            { typeof(ITourRepository), new TourRepository() },
            // { typeof(ITourService), new TourService() }

            { typeof(IAccommodationRepository), new AccommodationRepository() },
            // { typeof(IAccommodationService), new AccommodationService() }

            { typeof(ITourRepository), new TourRepository() },
            // { typeof(ITourService), new TourService() }

            { typeof(ITourRepository), new TourRepository() },
            // { typeof(ITourService), new TourService() }

            { typeof(IGuestReservationRepository), new GuestReservationRepository() },
               //{ typeof(IGuestReservationService), new GuestReservationService() }

           //  { typeof(IAccommodationRateRepository), new AccommodationRateRepository() },
               
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
