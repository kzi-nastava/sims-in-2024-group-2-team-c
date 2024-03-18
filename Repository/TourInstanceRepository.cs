using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class TourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourinstance.csv";

        private readonly Serializer<TourInstance> _serializer;

        private List<TourInstance> _tourInstances;

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourInstances = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourInstance Save(TourInstance tourInstance)
        {
            tourInstance.Id = NextId();
            _tourInstances = _serializer.FromCSV(FilePath);
            _tourInstances.Add(tourInstance);
            _serializer.ToCSV(FilePath, _tourInstances);
            return tourInstance;
        }

        public int NextId()
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            if (_tourInstances.Count < 1)
            {
                return 1;
            }
            return _tourInstances.Max(t => t.Id) + 1;
        }

        public void Delete(TourInstance tourInstance)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            TourInstance founded = _tourInstances.Find(t => t.Id == tourInstance.Id);
            _tourInstances.Remove(founded);
            _serializer.ToCSV(FilePath, _tourInstances);
        }

        public TourInstance Update(TourInstance tourInstance)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            TourInstance current = _tourInstances.Find(t => t.Id == tourInstance.Id);
            int index = _tourInstances.IndexOf(current);
            _tourInstances.Remove(current);
            _tourInstances.Insert(index, tourInstance);
            _serializer.ToCSV(FilePath, _tourInstances);
            return tourInstance;
        }

        public TourInstance FindByDate(DateTime date)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            TourInstance founded = _tourInstances.Find(d => d.Date.Date == date);
            return founded;
        }
    }
}
