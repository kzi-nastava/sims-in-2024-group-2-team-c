using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestNotificationRepository : ITourRequestNotificationRepository
    {

        private const string FilePath = "../../../Resources/Data/tourrequestnotifications.csv";

        private readonly Serializer<TourRequestNotification> _serializer;
        
        private List<TourRequestNotification> _notification;

        public TourRequestNotificationRepository()
        {
            _serializer = new Serializer<TourRequestNotification>();
            _notification = _serializer.FromCSV(FilePath);
        }

        public List<TourRequestNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequestNotification Save(TourRequestNotification notification)
        {
            notification.Id = NextId();
            _notification = _serializer.FromCSV(FilePath);
            _notification.Add(notification);
            _serializer.ToCSV(FilePath, _notification);
            return notification;
        }

        public int NextId()
        {
            _notification = _serializer.FromCSV(FilePath);
            if (_notification.Count < 1)
            {
                return 1;
            }
            return _notification.Max(t => t.Id) + 1;
        }

        public void Delete(TourRequestNotification notification)
        {
            _notification = _serializer.FromCSV(FilePath);
            TourRequestNotification founded = _notification.Find(t => t.Id == notification.Id);
            _notification.Remove(founded);
            _serializer.ToCSV(FilePath, _notification);
        }

        public TourRequestNotification Update(TourRequestNotification notification)
        {
            _notification = _serializer.FromCSV(FilePath);
            TourRequestNotification current = _notification.Find(t => t.Id == notification.Id);
            int index = _notification.IndexOf(current);
            _notification.Remove(current);
            _notification.Insert(index, notification);
            _serializer.ToCSV(FilePath, _notification);
            return notification;
        }
        public TourRequestNotification GetById(int id)
        {
            _notification = _serializer.FromCSV(FilePath);
            return _notification.Find(c => c.Id == id);

        }


    }
}
