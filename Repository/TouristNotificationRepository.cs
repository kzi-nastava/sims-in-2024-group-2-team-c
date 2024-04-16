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
    public class TouristNotificationRepository : ITouristNotificationRepository
    {

        private const string FilePath = "../../../Resources/Data/touristnotifications.csv";

        private readonly Serializer<TouristNotification> _serializer;

        private List<TouristNotification> _touristNotification;

        public TouristNotificationRepository()
        {
            _serializer = new Serializer<TouristNotification>();
            _touristNotification = _serializer.FromCSV(FilePath);
        }

        public List<TouristNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }


        public TouristNotification Save(TouristNotification touristNotification)
        {
            touristNotification.Id = NextId();
            _touristNotification = _serializer.FromCSV(FilePath);
            _touristNotification.Add(touristNotification);
            _serializer.ToCSV(FilePath, _touristNotification);
            return touristNotification;
        }

        public int NextId()
        {
            _touristNotification = _serializer.FromCSV(FilePath);
            if (_touristNotification.Count < 1)
            {
                return 1;
            }
            return _touristNotification.Max(c => c.Id) + 1;
        }

        public void Delete(TouristNotification touristNotification)
        {
            _touristNotification = _serializer.FromCSV(FilePath);
            TouristNotification founded = _touristNotification.Find(c => c.Id == touristNotification.Id);
            _touristNotification.Remove(founded);
            _serializer.ToCSV(FilePath, _touristNotification);
        }

        public TouristNotification Update(TouristNotification touristNotification)
        {
            _touristNotification = _serializer.FromCSV(FilePath);
            TouristNotification current = _touristNotification.Find(c => c.Id == touristNotification.Id);
            int index = _touristNotification.IndexOf(current);
            _touristNotification.Remove(current);
            _touristNotification.Insert(index, touristNotification);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _touristNotification);
            return touristNotification;
        }




    }
}
