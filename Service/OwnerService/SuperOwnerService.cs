using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.OwnerService
{
    public class SuperOwnerService
    {
        private OwnerRepository _ownerRepository;
        private AccommodationRateService  accommodationRateService;

        public SuperOwnerService(OwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
            accommodationRateService = new AccommodationRateService();
        }

        public void UpdateSuperhostStatus(int ownerId)
        {
            // Dobavljanje vlasnika iz repozitorijuma
            Owner owner = _ownerRepository.GetOwnerById(ownerId);

            if (owner != null)
            {
                // Učitavanje ocena smeštaja iz accommodationsRate.csv
                List<AccommodationRate> ratings = LoadRatingsFromCSV();

                // Filtriranje ocena samo za određenog vlasnika
                //var ownerRatings = ratings.Where(r => r.OwnerId == ownerId).ToList();
                var ownerRatings = ratings.Where(r => r.Reservation.Accommodation.Owner.Id == ownerId).ToList();

                // Računanje prosečne ocene
                double averageRating = ownerRatings.Any() ? ownerRatings.Average(r => (r.Cleanliness + r.OwnerRate) / 2) : 0;

                // Ažuriranje podataka o vlasniku
                owner.NumberOfRatings = ownerRatings.Count;
                owner.TotalRating = averageRating;

                // Ažuriranje statusa super domaćina
                if (owner.NumberOfRatings > 5 && owner.TotalRating > 4.5)
                {
                    owner.Super = true;
                }
                else
                {
                    owner.Super = false;
                }

                // Ažuriranje vlasnika u repozitorijumu
                _ownerRepository.UpdateOwner(owner);
            }
            else
            {
                throw new Exception("Owner not found");
            }
        }

        private List<AccommodationRate> LoadRatingsFromCSV()
        {
            return accommodationRateService.GetAll();
        }


        public Owner GetOwnerById(int ownerId)
        {
            // Pozivamo odgovarajuću metodu iz repozitorijuma kako bismo dobili vlasnika po ID-ju
            return _ownerRepository.GetOwnerById(ownerId);
        }
    }
}
