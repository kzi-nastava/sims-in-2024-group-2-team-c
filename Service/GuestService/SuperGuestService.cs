using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.GuestService
{
    public class SuperGuestService
    {
        private readonly IGuestRepository _guestRepository;
        //private readonly GuestReservationService _guestReservationService;
        //private readonly GuestReservationRepository guestReservationRepository;
        private readonly GuestReservationService _guestReservationService;

        public SuperGuestService()
        {
            _guestRepository = Injectorr.CreateInstance<IGuestRepository>();
            //guestReservationRepository = new GuestReservationRepository();
            _guestReservationService = new GuestReservationService();

        }

        public Guest GetGuest(int id)
        {
            List<GuestReservationDTO> guestReservations = _guestReservationService.GetAllGuestReservations(id);
            var reservationsInLastYear = guestReservations
                .Where(r => r.CheckIn >= DateTime.Now.AddYears(-1))
                .ToList();

            if (reservationsInLastYear.Count == 5)// == 10
            {
                Guest guest = new Guest
                {
                    Id = id,
                    SuperGuestStatus = true,
                    BonusPoints = 5
                };

                _guestRepository.Update(guest);

                return guest;
            }

            if (reservationsInLastYear.Count == 6)// == 11
            {
                Guest guest = new Guest
                {
                    Id = id,
                    SuperGuestStatus = true,
                    BonusPoints = 4
                };

                _guestRepository.Update(guest);

                return guest;
            }

            if (reservationsInLastYear.Count == 7)
            {
                Guest guest = new Guest
                {
                    Id = id,
                    SuperGuestStatus = true,
                    BonusPoints = 3
                };

                _guestRepository.Update(guest);

                return guest;
            }

            if (reservationsInLastYear.Count == 8)
            {
                Guest guest = new Guest
                {
                    Id = id,
                    SuperGuestStatus = true,
                    BonusPoints = 2
                };

                _guestRepository.Update(guest);

                return guest;
            }

            if (reservationsInLastYear.Count == 9)
            {
                Guest guest = new Guest
                {
                    Id = id,
                    SuperGuestStatus = true,
                    BonusPoints = 1
                };

                _guestRepository.Update(guest);

                return guest;
            }

            Guest falseGuest = new Guest
            {
                Id = id,
                SuperGuestStatus = false,
                BonusPoints = 0
            };

            _guestRepository.Update(falseGuest);

            return falseGuest;
        }

    }
}
