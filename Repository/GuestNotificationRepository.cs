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
    public class GuestNotificationRepository : IGuestNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/guestNotifications.csv";

        private readonly Serializer<GuestNotification> _serializer;

        private List<GuestNotification> _guestNotification;

        public GuestNotificationRepository()
        {
            _serializer = new Serializer<GuestNotification>();
            _guestNotification = _serializer.FromCSV(FilePath);
        }

        public List<GuestNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<GuestNotification> GetAllByGuestId(int guestId)
        {
            return _guestNotification.Where(n => n.GuestId == guestId).ToList();
        }

        public GuestNotification Save(GuestNotification guestNotification)
        {
            guestNotification.Id = NextId();
            _guestNotification = _serializer.FromCSV(FilePath);
            _guestNotification.Add(guestNotification);
            _serializer.ToCSV(FilePath, _guestNotification);
            return guestNotification;
        }

        public int NextId()
        {
            _guestNotification = _serializer.FromCSV(FilePath);
            if (_guestNotification.Count < 1)
            {
                return 1;
            }
            return _guestNotification.Max(c => c.Id) + 1;
        }

        public void Delete(GuestNotification guestNotification)
        {
            _guestNotification = _serializer.FromCSV(FilePath);
            GuestNotification founded = _guestNotification.Find(c => c.Id == guestNotification.Id);
            _guestNotification.Remove(founded);
            _serializer.ToCSV(FilePath, _guestNotification);
        }

        public GuestNotification Update(GuestNotification guestNotification)
        {
            _guestNotification = _serializer.FromCSV(FilePath);
            GuestNotification current = _guestNotification.Find(c => c.Id == guestNotification.Id);
            int index = _guestNotification.IndexOf(current);
            _guestNotification.Remove(current);
            _guestNotification.Insert(index, guestNotification);
            _serializer.ToCSV(FilePath, _guestNotification);
            return guestNotification;
        }
    }
}
