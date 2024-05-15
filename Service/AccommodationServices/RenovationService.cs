using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using BookingApp.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service.AccommodationServices
{
    public class RenovationService
    {
        private readonly IRenovationRepository renovationRepository;
        private AccommodationRepository _repository; 
        private ReservationRepository _reservationRepository;
        private IRenovationAvailableDateRepository _availableDateRepository;
       
        public RenovationService()
        {
            renovationRepository=Injectorr.CreateInstance<IRenovationRepository>();
            _availableDateRepository= Injectorr.CreateInstance<IRenovationAvailableDateRepository>();
            _repository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
        }

        public List<Accommodation> GetAccommodations()
        {
            return _repository.GetAll(); 
        }

        public void SaveRenovation(RenovationAvailableDate renovation)
        {
            _availableDateRepository.Save(renovation);
        }

        public void UpdateRenovation(RenovationAvailableDate renovation)
        {
            _availableDateRepository.Update(renovation);    
        }


        /* public List<Renovation> GetAllRenovations() //ovde vec imam metodu za dohvacanje renovacija(opseg datuma)
         {
             return renovationRepository.GetAll();
         }*/

        public void CancelRenovation(RenovationAvailableDate renovation)
        {
            if (renovation != null)
            {
                _availableDateRepository.Delete(renovation.Id);
            }
            else
            {
                MessageBox.Show("Please select a renovation to cancel.");
            }
        }


        public List<RenovationAvailableDate> GetAllRenovations() 
        {
            return _availableDateRepository.GetAll();
        }

        public void Save(Renovation renovation)
        {
            renovationRepository.Save(renovation);
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAll();
        }

        public List<AvailableDateDisplay> FindAvailableReservations(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int stayDuration)
        {
            List<Reservation> reservations = GetAllReservations();

            // Filtriranje rezervacija za odabrani smeštaj
            List<Reservation> accommodationReservations = reservations
                .Where(r => r.Accommodation.Name == selectedAccommodation.Name)
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
                    // Provera da li postoji preklapanje datuma sa postojećim rezervacijama
                    //bool hasConflict = accommodationReservations.Any(r =>
                    //    currentDate <= r.ArrivalDate.AddDays(1) && currentDate >= r.DepartureDate.AddDays(-stayDuration) && r.IsReserved);
                    bool hasConflict = accommodationReservations.Any(r =>
                        (currentDate >= r.ArrivalDate && currentDate <= r.DepartureDate)
                        || (currentDate.AddDays(stayDuration - 1) >= r.ArrivalDate && currentDate.AddDays(stayDuration - 1) <= r.DepartureDate)
                        || (currentDate <= r.ArrivalDate && currentDate.AddDays(stayDuration - 1) >= r.DepartureDate)
                        && r.IsReserved);


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




    }
}
