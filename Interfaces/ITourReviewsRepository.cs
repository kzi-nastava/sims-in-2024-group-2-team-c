using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourReviewsRepository
    {

        public List<TourReview> GetAll();
        public TourReview Save(TourReview tourReview);

        public int NextId();

        public void Delete(TourReview tourReview);

        public TourReview Update(TourReview tourReview);
        public TourReview GetById(int id);
    }
}
