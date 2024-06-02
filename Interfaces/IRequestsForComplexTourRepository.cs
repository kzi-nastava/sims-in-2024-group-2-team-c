using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IRequestsForComplexTourRepository
    {

        List<TourRequest> GetAll();
        TourRequest Save(TourRequest tourRequest);
        int NextId();
        void Delete(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);
        TourRequest GetById(int id);
    }
}
