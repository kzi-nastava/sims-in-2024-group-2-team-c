using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.AccommodationServices
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _repository;
        private readonly OwnerRepository _ownerRepository;

        public GuestRatingService()
        {
            _repository = Injectorr.CreateInstance<IGuestRatingRepository>();
            _ownerRepository = new OwnerRepository();
        }

        public GuestRating GetRatingsByOwnerUsername(string ownerUsername)
        {
            Owner owner = _ownerRepository.GetOwnerByUsername(ownerUsername);
            return _repository.GetRatingsByOwnerId(owner.Id);
        }
    }
}
