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
using BookingApp.Serializer;
using BookingApp.Repository;
using BookingApp.Injector;


namespace BookingApp.Service.AccommodationServices
{
    public class AccommodationRateService
    {

        private readonly IAccommodationRateRepository _repository;
        private readonly GuestReservationDTO _selectedReservation;

        private const string FilePath = "../../../Resources/Data/accommodationRate.csv";
        private readonly Serializer<AccommodationRate> _serializer;
        private List<AccommodationRate> _accommodationRates;
        private ReservationRepository reservationRepository;

        public List<AccommodationRate> AccommodationRates
        {
            get { return _accommodationRates; }
            set { _accommodationRates = value; }
        }

        public List<AccommodationRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRateService(IAccommodationRateRepository repository)
        {
            _repository = repository;

        }


        public AccommodationRateService()
        {
            // _repository = Injectorr.CreateInstance<IAccommodationRateRepository>();
            _accommodationRates = new List<AccommodationRate>();
            _serializer = new Serializer<AccommodationRate>();
            _accommodationRates = _serializer.FromCSV(FilePath);
            reservationRepository = new ReservationRepository();

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


        public void LoadAccommodationRatesFromCSV(string filePath)
        {
            _accommodationRates.Clear();

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                int Id = Convert.ToInt32(values[0]);
                int ReservationId = Convert.ToInt32(values[1]);
                int Cleanliness = Convert.ToInt32(values[2]);
                int OwnerRate = Convert.ToInt32(values[3]);
                string Comment = values[4];


                Reservation reservation = reservationRepository.GetReservationById(ReservationId);
                if (reservation != null)
                {
                    if (reservation != null && reservation.Guest != null)
                    {
                        string GuestUsername = reservation.Guest.Username;
                        Owner owner = reservation.Accommodation.Owner;
                        //string GuestUsername = GetGuestUsernameByReservationId(ReservationId); //mozes i na ovaj nacin

                        _accommodationRates.Add(new AccommodationRate
                        {
                            Id = Id,
                            Reservation = reservation,
                            GuestUsername = GuestUsername,
                            Cleanliness = Cleanliness,
                            OwnerRate = OwnerRate,
                            Comment = Comment
                            //Owner = owner ovo treba da dodam u accommodationRate
                        });
                    }
                }
                else
                {
                    Console.WriteLine($"Reservation with ID {ReservationId} not found.");
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