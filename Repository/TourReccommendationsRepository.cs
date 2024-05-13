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
    public class TourReccommendationsRepository : ITourReccommendationsRepository
    {

        private const string FilePath = "../../../Resources/Data/tourrecommendations.csv";
        
        private readonly Serializer<TourReccommendations> _serializer;

        private List<TourReccommendations> _reccommendations;

        public TourReccommendationsRepository()
        {
            _serializer = new Serializer<TourReccommendations>();
            _reccommendations = _serializer.FromCSV(FilePath);
        }

        public List<TourReccommendations> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }


        public TourReccommendations Save(TourReccommendations reccommendations)
        {
            reccommendations.Id = NextId();
            _reccommendations = _serializer.FromCSV(FilePath);
            _reccommendations.Add(reccommendations);
            _serializer.ToCSV(FilePath, _reccommendations);
            return reccommendations;
        }

        public int NextId()
        {
            _reccommendations = _serializer.FromCSV(FilePath);
            if (_reccommendations.Count < 1)
            {
                return 1;
            }
            return _reccommendations.Max(c => c.Id) + 1;
        }

        public void Delete(TourReccommendations reccommendations)
        {
            _reccommendations = _serializer.FromCSV(FilePath);
            TourReccommendations founded = _reccommendations.Find(c => c.Id == reccommendations.Id);
            _reccommendations.Remove(founded);
            _serializer.ToCSV(FilePath, _reccommendations);
        }

        public TourReccommendations Update(TourReccommendations reccommendations)
        {
            _reccommendations = _serializer.FromCSV(FilePath);
            TourReccommendations current = _reccommendations.Find(c => c.Id == reccommendations.Id);
            int index = _reccommendations.IndexOf(current);
            _reccommendations.Remove(current);
            _reccommendations.Insert(index, reccommendations);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _reccommendations);
            return reccommendations;
        }




    }
}
