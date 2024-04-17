﻿/*using BookingApp.DTO;
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
        private readonly ReservationDelayRepository _reservationDelayRepository;
        private readonly ReservationRepository _reservationRepository; //mislim da treba servis da pozivas
        public ReservationDelayService()
        {
            _reservationDelayRepository = new ReservationDelayRepository();
        }


        public ReservationDelayService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

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
                Guest = new Guest { Id = reservationDelayDTO.Guest.Id },
                Accommodation = new Accommodation { Id = reservationDelayDTO.Accommodation.Id },
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
                Accommodation = reservationDelay.Accommodation,
                NewCheckInDate = reservationDelay.NewCheckInDate,
                NewCheckOutDate = reservationDelay.NewCheckOutDate,
                Status = reservationDelay.Status
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

    }
}
*/