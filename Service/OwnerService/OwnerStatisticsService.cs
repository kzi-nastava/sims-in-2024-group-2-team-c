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

        public Dictionary<int, int> NumberOfReservationsByMonth(Accommodation accommodation, int year)
        {
            List<Reservation> reservations = _reservationService.GetAll()
                .Where(reservation => reservation.Accommodation.Name == accommodation.Name && reservation.ArrivalDate.Year == year)
                .ToList();

            var reservationsByMonth = reservations.GroupBy(reservation => reservation.ArrivalDate.Month)
                                                  .ToDictionary(group => group.Key, group => group.Count());

            EnsureAllMonthsPresent(reservationsByMonth);

            return reservationsByMonth;
        }


        public Dictionary<int, int> NumberOfCancellationsByMonth(Accommodation accommodation, int year)
        {
            List<Reservation> reservations = _reservationService.GetAll()
                .Where(reservation => reservation.Accommodation.Name == accommodation.Name && reservation.ArrivalDate.Year == year && !reservation.IsReserved)
                .ToList();

            var cancellationsByMonth = reservations.GroupBy(reservation => reservation.ArrivalDate.Month)
                                                   .ToDictionary(group => group.Key, group => group.Count());

            EnsureAllMonthsPresent(cancellationsByMonth);

            return cancellationsByMonth;
        }


        public Dictionary<int, int> NumberOfDelaysByMonth(Accommodation accommodation, int year)
        {
            List<ReservationDelay> delays = _reservationDelayService.GetAll()
                .Where(delay => delay.Accommodation.Name == accommodation.Name && delay.NewCheckInDate.Year == year)
                .ToList();

            var delaysByMonth = delays.GroupBy(delay => delay.NewCheckInDate.Month)
                                      .ToDictionary(group => group.Key, group => group.Count());

            EnsureAllMonthsPresent(delaysByMonth);

            return delaysByMonth;
        }


        private void EnsureAllMonthsPresent(Dictionary<int, int> dictionary)
        {
            for (int month = 1; month <= 12; month++)
            {
                if (!dictionary.ContainsKey(month))
                {
                    dictionary[month] = 0;
                }
            }
        }

        public double CalculateOccupancyRate(Accommodation accommodation)
        {
            List<Reservation> reservations = _reservationService.GetAll()
                .Where(reservation => reservation.Accommodation.Name == accommodation.Name)
                .ToList();

            
            int totalReservedNights = reservations.Sum(reservation => (reservation.DepartureDate - reservation.ArrivalDate).Days);

            
            int totalCapacityNights = 365 * accommodation.MaxGuests;

            if (totalCapacityNights == 0)
            {
                return 0;
            }

            double occupancyRate = (double)totalReservedNights / totalCapacityNights;
            return occupancyRate;
        }




        public List<OwnerStatistics> GetPopularLocations(List<OwnerStatistics> statistics)
        {
            return statistics.OrderByDescending(s => s.ReservationsByYear.Values.Sum())
                             .ThenByDescending(s => s.OccupancyRate)
                             .Take(3)
                             .ToList();
        }

        public List<OwnerStatistics> GetUnpopularLocations(List<OwnerStatistics> statistics)
        {
            return statistics.OrderBy(s => s.ReservationsByYear.Values.Sum())
                             .ThenBy(s => s.OccupancyRate)
                             .Take(3)
                             .ToList();
        }

    }
}
