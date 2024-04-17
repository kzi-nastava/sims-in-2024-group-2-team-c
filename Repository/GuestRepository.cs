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

        public string GetGuestUsernameById(int guestId)
        {
            // Pretraga gostiju po ID-ju
            Guest guest = guests.FirstOrDefault(g => g.Id == guestId);

            // Ako gost nije pronađen, vraćamo null
            if (guest == null)
            {
                return null;
            }

            // Vraćamo korisničko ime gosta
            return guest.Username;
        }

        public int GetGuestIdByUsername(string username)
        {
            // Pretraga gosta po korisničkom imenu
            Guest guest = guests.FirstOrDefault(g => g.Username == username);

            // Ako gost nije pronađen, vraćamo -1 kao indikator da gost nije pronađen
            if (guest == null)
            {
                return -1;
            }

            // Vraćamo ID pronađenog gosta
            return guest.Id;
        }

        public Guest GetGuestByUsername(string username)
        {
            // Pretraga gosta po korisničkom imenu
            return guests.FirstOrDefault(g => g.Username == username);
        }



    }
}
