using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    internal interface IGuestRatingRepository
    {
        public List<GuestRating> GetAll();
        public void Save(GuestRating guestRating);
        public void Delete(int id);
       // public int NextId();
    }
}
