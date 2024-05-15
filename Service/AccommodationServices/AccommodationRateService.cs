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
using BookingApp.Repository;
using BookingApp.Injector;
using System.Windows.Shapes;
using BookingApp.Service.AccommodationServices;


namespace BookingApp.Service.AccommodationServices
{
    public class AccommodationRateService
    {

        private readonly IAccommodationRateRepository _repository;
        private readonly GuestReservationDTO _selectedReservation;
        private List<AccommodationRate> _accommodationRates;

        private ReservationService  reservationService;
        private ReservationRepository reservationRepository ;


        public List<AccommodationRate> AccommodationRates
        {
            get { return _accommodationRates; }
            set { _accommodationRates = value; }
        }

        public List<AccommodationRate> GetAll()
        {
            return _repository.GetAll();
        }


         public AccommodationRateService()
         {
            _repository = Injectorr.CreateInstance<IAccommodationRateRepository>();

            reservationRepository = new ReservationRepository();
            _accommodationRates = new List<AccommodationRate>();
        }

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

                    Reservation = new Reservation() { Id = _selectedReservation.Id },
                    Cleanliness = cleanlinessRating,
                    OwnerRate = correctnessOfTheOwner,
                    Comment = comment

                };

                _repository.Save(data);


                MessageBox.Show("Accommodaiton and owner successfully rated.");

            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }

        public bool HasUserRatedAccommodation(int userId, String name)
        {
            return _repository.HasUserRatedAccommodation(userId, name);
        }


        public void LoadAccommodationRates()
        {
            _accommodationRates.Clear();

            List<AccommodationRate> accommodationRate = GetAll();
            foreach (AccommodationRate list in accommodationRate)
            {

                Reservation reservation = reservationRepository.GetReservationById(list.Reservation.Id);
                if (reservation != null)
                {
                    if (reservation != null && reservation.Guest != null)
                    {
                        string GuestUsername = reservation.Guest.Username;
                        Owner owner = reservation.Accommodation.Owner;
                        //string GuestUsername = GetGuestUsernameByReservationId(ReservationId); //mozes i na ovaj nacin

                        _accommodationRates.Add(new AccommodationRate
                        {
                            Id = list.Id,
                            Reservation = reservation,
                            GuestUsername = GuestUsername,
                            Cleanliness = list.Cleanliness,
                            OwnerRate = list.OwnerRate,
                            Comment = list.Comment
                            //Owner = owner ovo treba da dodam u accommodationRate
                        });
                    }
                }
                else
                {
                    Console.WriteLine($"Reservation with ID {list.Reservation.Id} not found.");
                }

            }
        }



        public string GetGuestUsernameByReservationId(int reservationId)
        {
            Reservation reservation = reservationRepository.GetReservationById(reservationId);
            if (reservation != null)
            {
                return reservation.Guest.Username;
            }
            else
            {
                return null;
            }
        }

    }

}