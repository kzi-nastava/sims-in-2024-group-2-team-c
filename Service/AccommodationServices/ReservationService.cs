using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.AccommodationServices
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService()
        {
            _reservationRepository = new ReservationRepository();
        }


        public Reservation FindOldReservation(Guest guest, Accommodation accommodation)
        {
            // Pretražite listu rezervacija da biste pronašli staru rezervaciju na osnovu gosta i smeštaja
            List<Reservation> reservations = _reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.Guest.Username == guest.Username && reservation.Accommodation.Name == accommodation.Name)
                {
                    return reservation;
                }
            }
            return null;
        }
    }
}