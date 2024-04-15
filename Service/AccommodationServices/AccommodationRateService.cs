using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service.AccommodationServices
{
    public class AccommodationRateService
    {

        private readonly IAccommodationRateRepository _repository;
        private readonly GuestReservationDTO _selectedReservation;

        public AccommodationRateService(GuestReservationDTO selectedReservation, IAccommodationRateRepository repository)
        {
            _selectedReservation = selectedReservation;
            _repository = repository;

        }

        public void RateAccommodation(int cleanlinessRating, int correctnessOfTheOwner, string comment)
        {

            try
            {
                var data = new AccommodationRate
                {
                    ReservationId = _selectedReservation.Id,
                    Cleanliness = cleanlinessRating,
                    OwnerRate = correctnessOfTheOwner,
                    Comment = comment
                };

                _repository.Save(data);


                MessageBox.Show("Guest rating saved successfully.");

            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

    }
}
