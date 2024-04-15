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
    public class TourReviewsRepository : ITourReviewsRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreviews.csv";

        private readonly Serializer<TourReview> _serializer;

        private List<TourReview> _tourReviews;

        public TourReviewsRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }


        public List<TourReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReview Save(TourReview tourReview)
        {
            tourReview.Id = NextId();
            _tourReviews = _serializer.FromCSV(FilePath);
            _tourReviews.Add(tourReview);
            _serializer.ToCSV(FilePath, _tourReviews);
            return tourReview;
        }

        public int NextId()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            if (_tourReviews.Count < 1)
            {
                return 1;
            }
            return _tourReviews.Max(c => c.Id) + 1;
        }

        public void Delete(TourReview tourReview)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            TourReview founded = _tourReviews.Find(c => c.Id == tourReview.Id);
            _tourReviews.Remove(founded);
            _serializer.ToCSV(FilePath, _tourReviews);
        }

        public TourReview Update(TourReview tourReview)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            TourReview current = _tourReviews.Find(c => c.Id == tourReview.Id);
            int index = _tourReviews.IndexOf(current);
            _tourReviews.Remove(current);
            _tourReviews.Insert(index, tourReview);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourReviews);
            return tourReview;
        }
        public TourReview GetById(int id)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews.Find(c => c.Id == id);

        }

    }
}
