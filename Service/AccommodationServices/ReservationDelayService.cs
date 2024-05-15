using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Injector;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BookingApp.Service
{
    public class ReservationDelayService
    {
        private readonly IReservationDelayRepository _reservationDelayRepository;
        private readonly ReservationRepository _reservationRepository; 

        public ReservationDelayService()
        {
            _reservationDelayRepository = Injectorr.CreateInstance<IReservationDelayRepository>();
            _reservationRepository = new ReservationRepository();
        }

        /*
        public ReservationDelayService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        */

        public bool IsNewDatesAvailable(int accommodationId, DateTime newCheckInDate, DateTime newCheckOutDate)
        {

            List<Reservation> reservations = _reservationRepository.GetReservationsByAccommodationId(accommodationId);


            foreach (Reservation reservation in reservations)
            {

                if (newCheckInDate < reservation.DepartureDate && newCheckOutDate > reservation.ArrivalDate)
                {

                    return false;

                }
            }
            return true;
        }

        public List<ReservationDelayDTO> GetAllReservationDelays()
        {
            var reservationDelays = _reservationDelayRepository.GetAll();
            return reservationDelays.Select(r => MapToDTO(r)).ToList();
        }

        public ReservationDelayDTO SaveReservationDelay(ReservationDelayDTO reservationDelayDTO)
        {
            var reservationDelay = MapToModel(reservationDelayDTO);
            var savedReservationDelay = _reservationDelayRepository.Save(reservationDelay);
            return MapToDTO(savedReservationDelay);
        }

        public ReservationDelayDTO GetReservationDelayById(int reservationId)
        {
            var reservationDelay = _reservationDelayRepository.GetById(reservationId);
            return MapToDTO(reservationDelay);
        }

        public void DeleteReservationDelay(int reservationId)
        {
            var reservationDelay = _reservationDelayRepository.GetById(reservationId);
            if (reservationDelay != null)
            {
                _reservationDelayRepository.Delete(reservationDelay);
            }
        }

        private ReservationDelay MapToModel(ReservationDelayDTO reservationDelayDTO)
        {
            return new ReservationDelay
            {
                ReservationDelayId = reservationDelayDTO.ReservationDelayId,
                Guest = reservationDelayDTO.Guest, //new Guest { Id = reservationDelayDTO.Guest.Id },
                Accommodation = reservationDelayDTO.Accommodation, //new Accommodation { Id = reservationDelayDTO.Accommodation.Id },
                NewCheckInDate = reservationDelayDTO.NewCheckInDate,
                NewCheckOutDate = reservationDelayDTO.NewCheckOutDate,
                Status = reservationDelayDTO.Status
            };
        }

        private ReservationDelayDTO MapToDTO(ReservationDelay reservationDelay)
        {
            return new ReservationDelayDTO
            {
                ReservationDelayId = reservationDelay.ReservationDelayId,
                Guest = reservationDelay.Guest,
                Accommodation = new Accommodation { Name = reservationDelay.Accommodation.Name, LocationDetails = reservationDelay.Accommodation.LocationDetails, Type = reservationDelay.Accommodation.Type },
                NewCheckInDate = reservationDelay.NewCheckInDate,
                NewCheckOutDate = reservationDelay.NewCheckOutDate,
                Status = reservationDelay.Status,
                Explanation = reservationDelay.Explanation
            };
        }

        public void UpdateReservationDelayStatus(int reservationDelayId, ReservationDelayStatus newStatus)
        {
            // Dobavljanje rezervacije iz repozitorijuma
            var reservationDelay = _reservationDelayRepository.GetById(reservationDelayId);

            if (reservationDelay != null)
            {
                // Ažuriranje statusa rezervacije
                reservationDelay.Status = newStatus;

                // Čuvanje ažurirane rezervacije u repozitorijumu
                _reservationDelayRepository.Update(reservationDelay);
            }
            else
            {
                // Ako rezervacija nije pronađena, možete baciti izuzetak ili izvršiti odgovarajuće radnje
                throw new InvalidOperationException("Reservation delay not found.");
            }
        }

        public Guest getGuestByReservation(GuestReservationDTO selectedReservation)
        {
            int id = selectedReservation.Id;
            Guest guest = _reservationRepository.getGuestByReservationId(id);
            return guest;
        }

        public Accommodation getAccommodationByReservation(GuestReservationDTO selectedReservation)
        {
            int id = selectedReservation.Id;
            return _reservationRepository.getAccommodationByReservation(id);
        }

        public List<ReservationDelay> GetAll()
        {
            return _reservationDelayRepository.GetAll();
        }

        public void Update(ReservationDelay selectedReservationDelay)
        {
            _reservationDelayRepository.Update(selectedReservationDelay);
        }
    }
}