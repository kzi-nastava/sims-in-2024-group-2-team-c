using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using BookingApp.Model;
using BookingApp.Interfaces;
using BookingApp.DTO;
using System.Windows;

namespace BookingApp.Repository
{
    class GuestReservationRepository : IGuestReservationRepository
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

        public List<GuestReservation> GetAll()
        {
            return serializer.FromCSV(filePath);
        }


        public string ReserveAccommodation(int accommodationId, int loggedInUserId, DateTime startDate, DateTime endDate, int stayDuration, DateTime checkInDate, DateTime checkOutDate, int numOfGuests)
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
                    GuestId = loggedInUserId,
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
        
        public List<GuestReservationDTO> GetAllGuestReservations(int guestId)
        {
            List<GuestReservationDTO> guestReservations = new List<GuestReservationDTO>();

            List<GuestReservation> reservations = serializer.FromCSV(filePath);

            List<Accommodation> accommodations = LoadAccommodations();

            List<Location> locations = LoadLocations();

            foreach (GuestReservation reservation in reservations)
            {
                if (reservation.GuestId == guestId)
                {
                    Accommodation accommodation = accommodations.FirstOrDefault(a => a.Id == reservation.Accommodation.Id);

                    if (accommodation != null)
                    {
                        Location accommodationLocation = locations.FirstOrDefault(l => l.Id == accommodation.Location.Id);

                        string location = $"{accommodationLocation.Country}, {accommodationLocation.City}";

                        GuestReservationDTO reservationDTO = new GuestReservationDTO
                        {
                            Id = reservation.ReservationId,
                            Name = accommodation.Name,
                            Location = location,
                            Type = accommodation.Type,
                            ImageUrl = accommodation.Images.Count > 0 ? accommodation.Images[0] : "",
                            CheckIn = reservation.CheckIn,
                            CheckOut = reservation.CheckOut
                        };

                        guestReservations.Add(reservationDTO);
                    }
                }
            }

            return guestReservations;
        }

        private List<Accommodation> LoadAccommodations()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            return accommodationRepository.GetAll();
        }

        private List<Location> LoadLocations()
        {
            LocationRepository locationRepository = new LocationRepository();
            return locationRepository.GetAll();
        }

        GuestReservation GetReservationById(int reservationId)
        {
            List<GuestReservation> reservations = serializer.FromCSV(filePath);
            return reservations.FirstOrDefault(reservation => reservation.ReservationId == reservationId);
        }

        public void UpdateReservation(GuestReservation reservation)
        {
            List<GuestReservation> reservations = serializer.FromCSV(filePath);

            int index = reservations.FindIndex(r => r.ReservationId == reservation.ReservationId);

            if (index != -1)
            {
                reservations[index] = reservation;

                serializer.ToCSV(filePath, reservations);
            }
            else
            {
                MessageBox.Show("Reservation not found!");
            }
        }

        public string CancelReservation(int reservationId)
        {
            try
            {
                var reservation = GetReservationById(reservationId);

                if (reservation != null)
                {
                    var accommodation = GetAccommodationById(reservation.Accommodation.Id);
                    var cancellationDeadline = CalculateCancellationDeadline(reservation);
                    var currentTime = DateTime.Now;

                    if (currentTime < cancellationDeadline)
                    {
                        reservation.IsReserved = false;
                        UpdateReservation(reservation);
                        return "Reservation successfully cancelled!";
                    }
                    else
                    {
                        string cancellationDaysMessage = accommodation != null ? $"Cancellation deadline is {accommodation.CancellationDays} days before check-in." : "Cancellation deadline is 24 hours before check-in.";
                        return $"Reservation cannot be cancelled as cancellation deadline has passed. {cancellationDaysMessage}";
                    }
                }
                else
                {
                    return "Reservation not found!";
                }
            }
            catch (Exception ex)
            {
                return $"Error cancelling reservation: {ex.Message}";
            }
        }

        private DateTime CalculateCancellationDeadline(GuestReservation reservation)
        {
            var accommodation = GetAccommodationById(reservation.Accommodation.Id);
            if (accommodation.CancellationDays != 0)
            {
                return reservation.CheckIn.AddDays(-accommodation.CancellationDays);
            }
            else
            {
                return reservation.CheckIn.AddHours(-24);
            }
        }

        public Accommodation GetAccommodationById(int accommodationId)
        {
            var accommodationRepository = new AccommodationRepository(); // Prilagodite ovo vašem stvarnom repozitorijumu
            return accommodationRepository.GetAccommodationById(accommodationId);
        }



    }


}




