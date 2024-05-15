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
    public class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/schedulingrenovations.csv";
        private readonly Serializer<Renovation> _serializer;
        private List<Renovation> _renovations;


        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public void Delete(int id)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation founded = _renovations.Find(r => r.Id == id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
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
