using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using BookingApp.Service.OwnerService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Service.AccommodationServices
{
    public class GuestReservationService
    {
        private readonly IGuestReservationRepository _guestReservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly OwnerRepository _ownerRepository;

        public GuestReservationService()
        {
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _ownerRepository = new OwnerRepository(); 
            _guestReservationRepository = Injectorr.CreateInstance<IGuestReservationRepository>(); ;    
        }


        public string ReserveAccommodation(int accommodationId, int loggedInUserId, DateTime startDate, DateTime endDate, int stayDuration, DateTime checkInDate, DateTime checkOutDate, int numOfGuests)
        {
            try
            {
                var reservations = _guestReservationRepository.GetAll();
                int maxReservationId = reservations.Count > 0 ? reservations.Max(r => r.ReservationId) : 0;

                bool isAccommodationAvailable = CheckAccommodationAvailability(accommodationId, checkInDate, checkOutDate, stayDuration, reservations);

                if (!isAccommodationAvailable)
                {
                    return "Accommodation is not available for the selected dates.";
                }

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
                    IsReserved = true
                };

                _guestReservationRepository.AddReservation(newReservation);

                return "Reservation successfully confirmed!";
            }
            catch (Exception ex)
            {
                return $"Error saving reservation: {ex.Message}";
            }
        }

        private bool CheckAccommodationAvailability(int accommodationId, DateTime checkInDate, DateTime checkOutDate, int stayDuration, List<GuestReservation> reservations)
        {
            var accommodationReservations = reservations.Where(r => r.Accommodation.Id == accommodationId);

            if (!accommodationReservations.Any())
            {
                return true;
            }

            var conflictingReservations = reservations.Where(r =>
                    r.Accommodation.Id == accommodationId &&
                    ((checkInDate >= r.CheckIn && checkInDate <= r.CheckOut) ||
                    (checkOutDate >= r.CheckIn && checkOutDate <= r.CheckOut) ||
                    (checkInDate <= r.CheckIn && checkOutDate >= r.CheckOut)));

            return !conflictingReservations.Any();
        }


        public List<AvailableDateDisplay> FindAvailableReservations(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int stayDuration)
        {
            List<GuestReservation> accommodationReservations = _guestReservationRepository.GetAll().Where(r => r.Accommodation.Id == selectedAccommodation.Id).ToList();

            List<AvailableDateDisplay> availableDates = new List<AvailableDateDisplay>();
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (IsDateAvailable(accommodationReservations, currentDate, endDate, stayDuration))
                {
                    availableDates.Add(new AvailableDateDisplay(currentDate, currentDate.AddDays(stayDuration - 1)));
                }

                currentDate = currentDate.AddDays(1);
            }

            return availableDates;
        }

        private bool IsDateAvailable(List<GuestReservation> accommodationReservations, DateTime currentDate, DateTime endDate, int stayDuration)
        {
            if (currentDate.AddDays(stayDuration - 1) > endDate)
                return false;

            for (int i = 0; i < stayDuration; i++)
            {
                DateTime checkDate = currentDate.AddDays(i);

                if (accommodationReservations.Any(r => checkDate <= r.CheckOut.AddDays(1) && checkDate >= r.CheckIn && r.IsReserved))
                {
                    return false;
                }
            }

            return true;
        }

        public List<GuestReservationDTO> GetAllGuestReservations(int guestId)
        {
            List<GuestReservationDTO> guestReservations = new List<GuestReservationDTO>();
            List<Accommodation> accommodations = _accommodationRepository.GetAll();
            List<Location> locations = _locationRepository.GetAll();
            List<GuestReservation> reservations = _guestReservationRepository.GetAll();

            foreach (GuestReservation reservation in reservations)
            {
                if (reservation.GuestId == guestId)
                {
                    Accommodation accommodation = accommodations.FirstOrDefault(a => a.Id == reservation.Accommodation.Id);

                    if (accommodation != null)
                    {
                        Location accommodationLocation = locations.FirstOrDefault(l => l.Id == accommodation.Location.Id);
                        string location = $"{accommodationLocation.Country}, {accommodationLocation.City}";

                        Owner owner = _ownerRepository.GetOwnerById(accommodation.Owner.Id);

                        GuestReservationDTO reservationDTO = new GuestReservationDTO
                        {
                            Id = reservation.ReservationId,
                            Name = accommodation.Name,
                            Location = location,
                            Type = accommodation.Type,
                            ImageUrl = accommodation.Images.Count > 0 ? accommodation.Images[0] : "",
                            CheckIn = reservation.CheckIn,
                            CheckOut = reservation.CheckOut,
                            OwnerUsername = owner.Username
                        };

                        guestReservations.Add(reservationDTO);
                    }
                }
            }

            return guestReservations;
        }

        public string CancelReservation(int reservationId)
        {
            try
            {
                return _guestReservationRepository.CancelReservation(reservationId);
            }
            catch (Exception ex)
            {
                return $"Error cancelling reservation: {ex.Message}";
            }
        }

        public bool GetIsReservedStatus(int reservationId)
        {
            try
            {
                return _guestReservationRepository.GetReservationStatus(reservationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting reservation status: {ex.Message}");
                return false;
            }
        }

    }
}
