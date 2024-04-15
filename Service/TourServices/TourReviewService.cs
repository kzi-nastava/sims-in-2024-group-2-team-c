using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourReviewService
    {
        private readonly ITourReviewsRepository _iTourReviewrepository;
        private readonly ITourReviewsRepository iTourRepository;
        //private readonly ITourInstanceRepository iTourInstanceRepository;
        //private TourInstanceService tourInstanceService;
        
        public TourReviewService()
        {
            _iTourReviewrepository = new TourReviewsRepository();
            //tourInstanceService = new(new TourInstanceRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }


        public void SaveReviews(int tourInstanceId,int guideId,int touristId, TourRatingDTO tourRatingDTO)
        {

            

            TourReview tourReview = new TourReview(tourInstanceId,24, guideId,touristId,tourRatingDTO.KnowledgeGrade,
                tourRatingDTO.LanguageGrade,tourRatingDTO.InterestingGrade, tourRatingDTO.Comment, false,tourRatingDTO.Images);
            _iTourReviewrepository.Save(tourReview);

        }


        public List<TourReview> GetAll()
        {
            return _iTourReviewrepository.GetAll();
        }


    }
}
