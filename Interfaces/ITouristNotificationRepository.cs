using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITouristNotificationRepository
    {


        public List<TouristNotification> GetAll();


        public TouristNotification Save(TouristNotification touristNotification);

        public int NextId();
        public void Delete(TouristNotification touristNotification);

        public TouristNotification Update(TouristNotification touristNotification);




    }
}
