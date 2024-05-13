using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourReccommendationsRepository
    {
        public List<TourReccommendations> GetAll();


        public TourReccommendations Save(TourReccommendations reccommendations);

        public int NextId();

        public void Delete(TourReccommendations reccommendations);
        public TourReccommendations Update(TourReccommendations reccommendations);
    }
}
