using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IGuestReservationRepository
    {

        //List<GuestReservation> GetAll();
        List<GuestReservationDTO> GetAllGuestReservations(int guestId);

        string CancelReservation(int reservationId);

        //public GuestReservation GetReservationById(int reservationId);

        //public void UpdateReservation(GuestReservation reservation);


    }
}
