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
    class TourInstanceRepository : ITourInstanceRepository
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
        /*
                public TourInstance Update(TourInstance tourInstance)
                {
                    _tourInstances = _serializer.FromCSV(FilePath);
                    TourInstance current = _tourInstances.Find(t => t.Id == tourInstance.Id);
                    int index = _tourInstances.IndexOf(current);
                    _tourInstances.Remove(current);
                    _tourInstances.Insert(index, tourInstance);
                    _serializer.ToCSV(FilePath, _tourInstances);
                    return tourInstance;
                }*/

        public TourInstance Update(TourInstance tourInstance)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            TourInstance current = _tourInstances.Find(t => t.Id == tourInstance.Id);
            if (current != null)
            {
                int index = _tourInstances.IndexOf(current);
                _tourInstances[index] = tourInstance;
                _serializer.ToCSV(FilePath, _tourInstances);
            }
            return tourInstance;
        }
        public TourInstance GetByTourId(int id) 
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            TourInstance founded = _tourInstances.Find(t => t.IdTour == id);
            return founded;
        }

        public List<TourInstance> FindByDate(DateTime date)
        {
            _tourInstances = _serializer.FromCSV(FilePath).ToList();
            DateTime searchDate = date.Date;
            string formattedDate = searchDate.ToString("dd.MM.yyyy.");

            List<TourInstance> foundedInstances = new List<TourInstance>();
            foreach(TourInstance instance in  _tourInstances)
            {
                if (instance.Date.ToString("dd.MM.yyyy.") == formattedDate)
                {
                    foundedInstances.Add(instance);
                }
            }
            return foundedInstances;
        }
        
        /*public TourInstance FindByDate(DateTime date)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            DateTime searchDate = date.Date;
            TourInstance founded = _tourInstances.Find(d => d.Date.Date.Equals(searchDate));
            return founded;
        }

        public List<TourInstance> FindByDate(DateTime date)
        {
            _tourInstances = _serializer.FromCSV(FilePath);
            DateTime searchDate = date.Date;
            List<TourInstance> founded = _tourInstances.FindAll(t => t.Date == searchDate);
            //TourInstance founded = _tourInstances.Find(d => d.Date.Date.Equals(searchDate));
            return founded;
        }*/

        public List<TourInstance> GetTourInstancesByTourId(int tourId)
        {
            List<TourInstance> tourInstances = new List<TourInstance>();

            // Iterate through all tour instances and filter out the ones with matching tour ID
            foreach (TourInstance tourInstance in _tourInstances)
            {
                if (tourInstance.IdTour == tourId)
                {
                    tourInstances.Add(tourInstance);
                }
            }

            return tourInstances;
        }

        public List<TourInstance> GetInstancesByTourIdAndAvailableSlots(int tourId, int? numberOfPeople)
        {
            // Retrieve instances for the specified tour ID
            List<TourInstance> instances = GetAll().Where(instance => instance.IdTour == tourId).ToList();

            // Filter instances based on available slots if numberOfPeople is specified
            if (numberOfPeople.HasValue)
            {
                instances = instances.Where(instance => (instance.MaxTourists - instance.ReservedTourists) >= numberOfPeople.Value).ToList();
            }

            return instances;
        }

    }
}
