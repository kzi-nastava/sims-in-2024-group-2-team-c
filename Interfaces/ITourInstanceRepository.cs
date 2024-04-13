using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    internal interface ITourInstanceRepository
    {
        List<TourInstance> GetAll();
        TourInstance Save(TourInstance tourI);
        int NextId();
        void Delete(TourInstance tour);
        TourInstance Update(TourInstance tour);
        //TourInstance GetById(int id);
        //List<TourInstance> GetToursByLocationId(int locationId);
    }
}
