using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        List<Accommodation> GetToursByLocationId(int locationId);
        List<Accommodation> GetAccommodationsByName(string searchTerm);
        List<Accommodation> GetAccommodationsByType(string type);
        List<Accommodation> GetAccommodationsByNumOfGuests(string numOfGuestsStr);
        List<Accommodation> GetAccommodationsByBookingDays(string bookingDaysStr);
        Accommodation Save(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        Accommodation GetAccommodationById(int id);
    }
}
