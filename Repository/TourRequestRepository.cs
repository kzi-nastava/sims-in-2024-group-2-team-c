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
    internal class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourrequest.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest founded = _tourRequests.Find(t => t.Id == tourRequest.Id);
            _tourRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public List<TourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequest GetById(int id)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.Find(c => c.Id == id);
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(t => t.Id) + 1;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequests.Find(t => t.Id == tourRequest.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }
    }
}
