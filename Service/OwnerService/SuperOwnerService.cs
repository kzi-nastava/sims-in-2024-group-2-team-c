using BookingApp.Model;
using BookingApp.Repository;
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

        public SuperOwnerService(OwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        
        public void UpdateSuperhostStatus(int ownerId)
        {
            Owner owner = _ownerRepository.GetOwnerById(ownerId); 
            if (owner != null)
            {
               
                double averageRating = (owner.TotalRating * owner.NumberOfRatings) / (owner.NumberOfRatings + 1);

                
                owner.NumberOfRatings++;

                
                owner.TotalRating = ((owner.TotalRating * (owner.NumberOfRatings - 1)) + averageRating) / owner.NumberOfRatings;

                
                if (owner.NumberOfRatings > 1 && owner.TotalRating > 4.5)
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
        public Owner GetOwnerById(int ownerId)
        {
            // Pozivamo odgovarajuću metodu iz repozitorijuma kako bismo dobili vlasnika po ID-ju
            return _ownerRepository.GetOwnerById(ownerId);
        }
    }
}
