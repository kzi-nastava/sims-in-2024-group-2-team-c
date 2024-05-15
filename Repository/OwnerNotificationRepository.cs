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
    public class OwnerNotificationRepository : IOwnerNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/ownerNotifications.csv";

        private readonly Serializer<OwnerNotification> _serializer;

        private List<OwnerNotification> _ownerNotification;

        public OwnerNotificationRepository()
        {
            _serializer = new Serializer<OwnerNotification>();
            _ownerNotification = _serializer.FromCSV(FilePath);
        }

        public List<OwnerNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<OwnerNotification> GetAllByOwnerId(int ownerId)
        {
            return _ownerNotification.Where(n => n.OwnerId == ownerId).ToList();
        }

        public OwnerNotification Save(OwnerNotification ownerNotification)
        {
            ownerNotification.Id = NextId();
            _ownerNotification = _serializer.FromCSV(FilePath);
            _ownerNotification.Add(ownerNotification);
            _serializer.ToCSV(FilePath, _ownerNotification);
            return ownerNotification;
        }

        public int NextId()
        {
            _ownerNotification = _serializer.FromCSV(FilePath);
            if (_ownerNotification.Count < 1)
            {
                return 1;
            }
            return _ownerNotification.Max(c => c.Id) + 1;
        }

        public void Delete(OwnerNotification ownerNotification)
        {
            _ownerNotification = _serializer.FromCSV(FilePath);
            OwnerNotification founded = _ownerNotification.Find(c => c.Id == ownerNotification.Id);
            _ownerNotification.Remove(founded);
            _serializer.ToCSV(FilePath, _ownerNotification);
        }

        public OwnerNotification Update(OwnerNotification ownerNotification)
        {
            _ownerNotification = _serializer.FromCSV(FilePath);
            OwnerNotification current = _ownerNotification.Find(c => c.Id == ownerNotification.Id);
            int index = _ownerNotification.IndexOf(current);
            _ownerNotification.Remove(current);
            _ownerNotification.Insert(index, ownerNotification);
            _serializer.ToCSV(FilePath, _ownerNotification);
            return ownerNotification;
        }
    }
}
