using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;

namespace BookingApp.Service.OwnerService
{
    public class OwnerStatisticsService
    {
        private ReservationDelayService _reservationDelayService;
        private ReservationService _reservationService;
        private List<Reservation> _reservations;

        public OwnerStatisticsService()
        {
            _reservationDelayService = new ReservationDelayService();
            _reservationService = new ReservationService();
            _reservations = new List<Reservation>();
        }

        public Dictionary<int, int> NumberOfReservationsByYear(Accommodation accommodation)
        {
            List<Reservation> reservations = _reservationService.GetAll().Where(reservation => reservation.Accommodation.Name == accommodation.Name).ToList();
            // Grupišemo rezervacije po godinama
            Dictionary<int, int> reservationsByYear = reservations.GroupBy(reservation => reservation.ArrivalDate.Year)
                                                 .ToDictionary(group => group.Key, group => group.Count());
            if (!reservationsByYear.ContainsKey(2022))
                reservationsByYear[2022] = 0;

            if (!reservationsByYear.ContainsKey(2023))
                reservationsByYear[2023] = 0;

            if (!reservationsByYear.ContainsKey(2024))
                reservationsByYear[2024] = 0;

            return reservationsByYear;
        }

        public Dictionary<int, int> NumberOfCancellationsByYear(Accommodation accommodation)
        {
            List<Reservation> reservations = _reservationService.GetAll().Where(reservation => reservation.Accommodation.Name == accommodation.Name).ToList();
            // Grupišemo rezervacije koje su otkazane po godinama
            var cancellationsByYear = reservations.Where(reservation => !reservation.IsReserved)
                                                  .GroupBy(reservation => reservation.ArrivalDate.Year)
                                                  .ToDictionary(group => group.Key, group => group.Count());

            if (!cancellationsByYear.ContainsKey(2022))
                cancellationsByYear[2022] = 0;

            if (!cancellationsByYear.ContainsKey(2023))
                cancellationsByYear[2023] = 0;

            if (!cancellationsByYear.ContainsKey(2024))
                cancellationsByYear[2024] = 0;
            return cancellationsByYear;
        }

        public Dictionary<int, int> NumberOfDelaysByYear(Accommodation accommodation)
        {
            List<ReservationDelay> delays = _reservationDelayService.GetAll().Where(delay => delay.Accommodation.Name == accommodation.Name).ToList();
            // Grupišemo odlaganja po godinama
            var delaysByYear = delays.GroupBy(delay => delay.NewCheckInDate.Year)
                                     .ToDictionary(group => group.Key, group => group.Count());
            if (!delaysByYear.ContainsKey(2022))
                delaysByYear[2022] = 0;

            if (!delaysByYear.ContainsKey(2023))
                delaysByYear[2023] = 0;

            if (!delaysByYear.ContainsKey(2024))
                delaysByYear[2024] = 0;
            return delaysByYear;
        }

       


    }
}
