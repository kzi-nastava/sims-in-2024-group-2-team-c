﻿using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Service.TourServices
{
    public class TourReviewService
    {

        private readonly ITourReviewsRepository iTourReviewRepository;
        private TourInstanceService tourInstanceService;
        private TourService tourService;
        private TouristService touristService;
        private KeyPointService keyPointService;
   
        public TourReviewService()
        {
            iTourReviewRepository = Injectorr.CreateInstance<ITourReviewsRepository>();
            tourInstanceService = new TourInstanceService();
            tourService = new TourService();
            touristService = new TouristService();
            keyPointService = new KeyPointService();
        
        }



        public void SaveReviews(int tourInstanceId,int guideId,int touristId, TourRatingDTO tourRatingDTO)
        {

            

            TourReview tourReview = new TourReview(tourInstanceId,24, guideId,touristId,tourRatingDTO.KnowledgeGrade,
                tourRatingDTO.LanguageGrade,tourRatingDTO.InterestingGrade, tourRatingDTO.Comment, false,tourRatingDTO.Images);
            iTourReviewRepository.Save(tourReview);

        }


        public List<TourReview> GetAll()
        {
            return iTourReviewRepository.GetAll();
        }
        public TourReview GetById(int id)
        {
            return iTourReviewRepository.GetById(id);
        }
        public TourReview Update(TourReview tourReview)
        {
            return iTourReviewRepository.Update(tourReview);
        }
        public List<TourReviewDTO> GetReviewDTOs()
        {
            List<TourReviewDTO> founded = new List<TourReviewDTO>();
            List<TourReview> reviews = GetAll();
            foreach (TourReview review in reviews)
            {
                TourInstance instance = tourInstanceService.GetById(review.TourInstanceId);
                Tour tour = tourService.GetById(instance.IdTour);
                Tourist tourist = touristService.GetById(review.TouristId);
                KeyPoint kp = keyPointService.GetById(review.KeyPointId);
                TourReviewDTO dto = new TourReviewDTO
                {
                    Id = review.Id,
                    TourName = tour.Name,
                    Tourist = tourist.Username,
                    Comment = review.Comment,
                    KnowledgeGrade = review.KnowledgeGrade,
                    LanguageGrade = review.LanguageGrade,
                    InterestingGrade = review.InterestingGrade,
                    StartKeyPoint = kp.Name,
                    Reported = review.Reported,
                    Date = instance.Date,
                    Images = review.Images
                };
                founded.Add(dto);
            }
            return founded;
        }
        public Tour GetTourByName(string name)
        {
            List<Tour> tours = tourService.GetAll();
            Tour found = null;
            foreach(Tour tour in tours)
            {
                if (tour.Name == name)
                {
                    found = tour;
                }
            }
            return found;
        }
        public float CalculateScoreByLanguage(string language)
        {
            List<TourReviewDTO> reviews = GetReviewDTOs();
            float score = 0;
            foreach(TourReviewDTO dto in reviews)
            {
                Tour tour = GetTourByName(dto.TourName);
                if(tour.Language == language)
                {
                    score += (float)((dto.LanguageGrade + dto.KnowledgeGrade + dto.InterestingGrade) / 3);
                }
            }
            return score;
        }
        public bool ReportReview(TourReviewDTO selectedReview)
        {
            TourReview review = GetById(selectedReview.Id);
            KeyPoint startedPoint = keyPointService.GetById(review.KeyPointId);
            foreach(int touristsId in startedPoint.PeopleIds)
            {
                if(!startedPoint.PeopleIds.Contains(review.TouristId))
                {
                    review.Reported = true;
                    Update(review);
                    return true;
                }
                    
            }
            return false;
        }
    }
}
