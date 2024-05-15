using BookingApp.DTO;
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
        public GuestReservationService(IGuestReservationRepository guestReservationRepository)
        {
            _guestReservationRepository = guestReservationRepository;        }


        public List<GuestReservationDTO> GetAllGuestReservations(int guestId)
        {

            return _guestReservationRepository.GetAllGuestReservations(guestId);
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
                return false; // Ako se desi greška, vraćamo false
            }
        }

    }
}
