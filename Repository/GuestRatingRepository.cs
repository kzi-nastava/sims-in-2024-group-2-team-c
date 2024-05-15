using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
   public class GuestRatingRepository : IGuestRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/guestratings.csv";
        private readonly Serializer<GuestRating> _serializer;
        private List<GuestRating> _guestRatings;

        public GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _guestRatings = _serializer.FromCSV(FilePath);
        }

        public List<GuestRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(GuestRating guestRating)
        {
            guestRating.Id = NextId(); 
            _guestRatings = _serializer.FromCSV(FilePath);
            _guestRatings.Add(guestRating);
            _serializer.ToCSV(FilePath, _guestRatings);
        }

        public void Delete(int id)
        {
            _guestRatings = _serializer.FromCSV(FilePath);
            GuestRating founded = _guestRatings.Find(r => r.Id == id);
            _guestRatings.Remove(founded);
            _serializer.ToCSV(FilePath, _guestRatings);
        }

        public int NextId()
        {
            _guestRatings = _serializer.FromCSV(FilePath);
            if (_guestRatings.Count < 1)
            {
                return 1;
            }
            return _guestRatings.Max(r => r.Id) + 1;
        }

        public GuestRating GetRatingsByOwnerId(int Id)
        {
            GuestRating rating = _guestRatings.Find(r => r.Owner.Id == Id);
            return rating;
        }

    }

}
