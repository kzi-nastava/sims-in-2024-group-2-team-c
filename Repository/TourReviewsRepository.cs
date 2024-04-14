using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class TourReviewsRepository : ITourReviewsRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreviews.csv";

        private readonly Serializer<TourReview> _serializer;

        private List<TourReview> _tourReviews;

        public TourReviewsRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        /*public void Delete(TourReview tourReview)
        {
            throw new NotImplementedException();
        }

        public List<TourReview> GetAll()
        {
            //return TourReviewsRepository.GetAll();
        }

        public TourReview GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            throw new NotImplementedException();
        }

        public TourReview Save(TourReview tourReview)
        {
            throw new NotImplementedException();
        }

        public TourReview Update(TourReview tourReview)
        {
            throw new NotImplementedException();
        }*/
    }
}
