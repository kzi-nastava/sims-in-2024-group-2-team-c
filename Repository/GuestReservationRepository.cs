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
        private Serializer<GuestReservation> serializer;
        List<GuestReservation> reservations;

        public GuestReservationRepository()
        {
            serializer = new Serializer<GuestReservation>();
            reservations = serializer.FromCSV(filePath);
        }

        public List<GuestReservation> GetAll()
        {
            return reservations;
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
            var accommodationRepository = new AccommodationRepository();
            return accommodationRepository.GetAccommodationById(accommodationId);
        }

        public bool GetReservationStatus(int reservationId)
        {
            try
            {
                var reservation = GetReservationById(reservationId);

                if (reservation != null)
                {
                    return reservation.IsReserved;
                }
                else
                {
                    MessageBox.Show("Reservation not found!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting reservation status: {ex.Message}");
                return false;
            }
        }

        public void AddReservation(GuestReservation reservation)
        {
            try
            {
                reservations.Add(reservation);
                serializer.ToCSV(filePath, reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding reservation: {ex.Message}");
            }
        }

    }



}




