using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IGuestRepository
    {
        public List<Guest> GetAll();
        public Guest Save(Guest guest);

        public int NextId();

        public Guest Update(Guest guest);

        public Guest GetGuestById(int guestId);

        public string GetGuestUsernameById(int guestId);

        public int GetGuestIdByUsername(string username);

        public Guest GetGuestByUsername(string username);


    }
}
