using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourRequestNotificationRepository
    {


        public List<TourRequestNotification> GetAll();

        public TourRequestNotification Save(TourRequestNotification notification);

        public int NextId();

        public void Delete(TourRequestNotification notification);

        public TourRequestNotification Update(TourRequestNotification notification);
        public TourRequestNotification GetById(int id);
        
    }
}
