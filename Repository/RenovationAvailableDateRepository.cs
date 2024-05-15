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
    public class RenovationAvailableDateRepository : IRenovationAvailableDateRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<RenovationAvailableDate> _serializer;
        private List<RenovationAvailableDate> _renovations;

        public RenovationAvailableDateRepository()
        {
            _serializer = new Serializer<RenovationAvailableDate>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        public List<RenovationAvailableDate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(RenovationAvailableDate renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public void Delete(int id)
        {
            _renovations = _serializer.FromCSV(FilePath);
            RenovationAvailableDate founded = _renovations.Find(r => r.Id == id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public void Update(RenovationAvailableDate renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            RenovationAvailableDate existingRenovation = _renovations.FirstOrDefault(r => r.Id == renovation.Id);
            if (existingRenovation != null)
            {
                int index = _renovations.IndexOf(existingRenovation);
                _renovations[index] = renovation;
                _serializer.ToCSV(FilePath, _renovations);
            }
            else
            {
                throw new ArgumentException($"Renovation with ID {renovation.Id} does not exist.");
            }
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(r => r.Id) + 1;
        }

    }
}
