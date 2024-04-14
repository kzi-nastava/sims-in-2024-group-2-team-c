using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourInstanceRepository
    {

        public List<TourInstance> GetAll();
        public TourInstance Save(TourInstance tourInstance);
        public int NextId();
        public void Delete(TourInstance tourInstance);
        public TourInstance Update(TourInstance tourInstance);
        public TourInstance GetByTourId(int id);
        public List<TourInstance> FindByDate(DateTime date);
        public List<TourInstance> GetTourInstancesByTourId(int tourId);
        public List<TourInstance> GetInstancesByTourIdAndAvailableSlots(int tourId, int? numberOfPeople);

    }
}
