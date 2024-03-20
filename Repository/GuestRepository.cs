using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestRepository
    {

        private const string FilePath = "../../../Resources/Data/guests.csv";
        private readonly Serializer<Guest> _serializer;
        private List<Guest> guests;

        public GuestRepository()
        {
            _serializer = new Serializer<Guest>();
            guests = _serializer.FromCSV(FilePath);
        }

        public List<Guest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guest Save(Guest guest)
        {
            guest.Id = NextId();
            guests = _serializer.FromCSV(FilePath);
            guests.Add(guest);
            _serializer.ToCSV(FilePath, guests);
            return guest;
        }


        public int NextId()
        {
            List<Guest> guests = _serializer.FromCSV(FilePath);
            if (guests.Count < 1)
            {
                return 1;
            }
            return guests.Max(guest => guest.Id) + 1;
        }

        public Guest GetGuestById(int guestId)
        {
            // Pretraga gostiju po ID-ju
            return guests.FirstOrDefault(g => g.Id == guestId);
        }


     


    }
}
