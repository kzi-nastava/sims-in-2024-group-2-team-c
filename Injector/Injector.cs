using BookingApp.Interfaces;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Injector
{
    public class Injector
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

            {typeof(ITourInstanceRepository), new TourInstanceRepository() },
           // {typeof(FollowTourService), new FollowTourService() },

            

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
