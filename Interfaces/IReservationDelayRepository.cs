using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IReservationDelayRepository
    {
        public List<ReservationDelay> GetAll();

        public ReservationDelay Save(ReservationDelay reservationDelay);

        public void Delete(ReservationDelay reservationDelay);

        public ReservationDelay GetById(int reservationId);

        public void Update(ReservationDelay updatedReservationDelay);




    }
}
