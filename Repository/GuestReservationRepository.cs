using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using BookingApp.Model;

namespace BookingApp.Repository
{
    class GuestReservationRepository
    {

        private string filePath = "../../../Resources/Data/guestReservations.csv";
        private Serializer<GuestReservation> serializer = new Serializer<GuestReservation>();


        // prikazuje alternativne datume dva dana nakon poslednjeg zauzetog -- to ne valja
        public List<AvailableDateDisplay> FindAvailableReservations(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int stayDuration)
        {
            List<GuestReservation> reservations = serializer.FromCSV(filePath);

            // Filtriranje rezervacija za odabrani smeštaj
            List<GuestReservation> accommodationReservations = reservations
                .Where(r => r.Accommodation.Id == selectedAccommodation.Id)
                .ToList();

            // Pronalaženje dostupnih datuma u opsegu za dati smeštaj
            List<AvailableDateDisplay> availableDates = new List<AvailableDateDisplay>();
            // Prolazak kroz svaki dan u opsegu
            DateTime currentDate = startDate;

            while (currentDate <= endDate) // Ovde se uzima u obzir ceo opseg datuma
            {
                // Provera dostupnosti za svaki dan u opsegu boravka
                bool isDateAvailable = true;

                // Provera da li je trenutni datum unutar opsega boravka
                if (currentDate.AddDays(stayDuration - 1) <= endDate)
                {
                    bool hasConflict = false;
                    for (int i = 0; i < stayDuration; i++)
                    {
                        DateTime checkDate = currentDate.AddDays(i);

                        // Provera da li postoji preklapanje datuma sa postojećim rezervacijama
                        bool conflictForDate = accommodationReservations.Any(r =>
                            checkDate <= r.CheckOut.AddDays(1) && checkDate >= r.CheckIn && r.IsReserved);

                        if (conflictForDate)
                        {
                            hasConflict = true;
                            break; // Prekidamo petlju jer smo našli zauzet dan
                        }
                    }

                    if (hasConflict)
                    {
                        isDateAvailable = false;
                    }
                }
                else
                {
                    isDateAvailable = false; // Datum je van opsega boravka, ne dodajemo ga u alternativne datume
                }

                // Ako je datum dostupan i nije unutar zadanog opsega, dodajemo ga u listu dostupnih datuma
                if (isDateAvailable && currentDate > DateTime.Now) // Dodajemo samo buduće datume
                {
                    availableDates.Add(new AvailableDateDisplay(currentDate, currentDate.AddDays(stayDuration - 1)));
                }

                // Prelazimo na sledeći dan
                currentDate = currentDate.AddDays(1);
            }

            return availableDates;
        }
        

        public string ReserveAccommodation(int accommodationId, DateTime startDate, DateTime endDate, int stayDuration, DateTime checkInDate, DateTime checkOutDate, int numOfGuests)
        {
            try
            {
                List<GuestReservation> reservations = serializer.FromCSV(filePath);

                // Pronalazak maksimalnog ID-a u postojećim rezervacijama
                int maxReservationId = reservations.Count > 0 ? reservations.Max(r => r.ReservationId) : 0;

                // Provera dostupnosti smeštaja za navedene datume i boravak00
                bool isAccommodationAvailable = CheckAccommodationAvailability(accommodationId, checkInDate, checkOutDate, stayDuration, reservations);

                if (!isAccommodationAvailable)
                {
                    return "Accommodation is not available for the selected dates.";
                }
                
                // Generisanje novog ID-a za rezervaciju
                int newReservationId = maxReservationId + 1;

                GuestReservation newReservation = new GuestReservation()
                {
                    ReservationId = newReservationId,
                    Accommodation = new Accommodation() { Id = accommodationId },
                    StartDate = startDate,
                    EndDate = endDate,
                    StayDurationInDays = stayDuration,
                    CheckIn = checkInDate,
                    CheckOut = checkOutDate,
                    NumGuests = numOfGuests,
                    IsReserved = true // Označavanje rezervacije kao potvrđene
                };

                reservations.Add(newReservation);
                serializer.ToCSV(filePath, reservations);

                return "Reservation successfully confirmed!";
            }
            catch (Exception ex)
            {
                return $"Error saving reservation: {ex.Message}";
            }
        }


        private bool CheckAccommodationAvailability(int accommodationId, DateTime checkIn, DateTime checkOut, int stayDuration, List<GuestReservation> reservations)
        {

            // Provera dostupnosti smeštaja kada nema postojećih rezervacija za taj smeštaj
            var accommodationReservations = reservations.Where(r => r.Accommodation.Id == accommodationId);

            if (!accommodationReservations.Any())
            {
                return true; // Nema postojećih rezervacija, smeštaj je dostupan
            }

            var conflictingReservations = reservations.Where(r =>
                    r.Accommodation.Id == accommodationId &&
                    ((checkIn >= r.CheckIn && checkIn <= r.CheckOut) || // Provera preklapanja datuma
                    (checkOut >= r.CheckIn && checkOut <= r.CheckOut) ||
                    (checkIn <= r.CheckIn && checkOut >= r.CheckOut)));

                if (conflictingReservations.Any())
                {
                    return false; // Smeštaj je zauzet u navedenom periodu
                }

                // Provera dostupnosti na nivou dana uzimajući u obzir stayDuration
                foreach (var reservation in reservations)
                {
                    // Provera samo za rezervacije koje se odnose na isti smeštaj
                    if (reservation.Accommodation.Id == accommodationId)
                    {
                        // Provera slobodnih dana između checkIn i checkOut datuma
                        DateTime startDate = reservation.CheckIn;
                        DateTime endDate = reservation.CheckOut.AddDays(-stayDuration); // Krajnji datum za koji smeštaj mora biti slobodan

                        // Ako postoji slobodan period unutar checkIn i checkOut datuma, smeštaj je dostupan
                        if (startDate >= checkOut || endDate <= checkIn)
                        {
                            return true;
                        }
                    }
                }

                return false; // Smeštaj nije dostupan za rezervaciju
        }


    }
}


