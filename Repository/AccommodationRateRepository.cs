using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRateRepository : IAccommodationRateRepository
    {

        private const string FilePath = "../../../Resources/Data/accommodationRate.csv";
        private readonly Serializer<AccommodationRate> _serializer;
        private List<AccommodationRate> _accommodationRates;

        public AccommodationRateRepository()
        {
            _serializer = new Serializer<AccommodationRate>();
            _accommodationRates = _serializer.FromCSV(FilePath);
        }

        
         public void Save(AccommodationRate accommodationRate)
        {
            accommodationRate.Id = NextId();
            _accommodationRates = _serializer.FromCSV(FilePath);
            _accommodationRates.Add(accommodationRate);
            _serializer.ToCSV(FilePath, _accommodationRates);
        }

        public int NextId()
        {
            _accommodationRates = _serializer.FromCSV(FilePath);
            if (_accommodationRates.Count < 1)
            {
                return 1;
            }
            return _accommodationRates.Max(a => a.Id) + 1;
        }

    }
}
