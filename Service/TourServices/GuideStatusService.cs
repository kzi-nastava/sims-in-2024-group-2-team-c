using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Service.TourServices
{
    public class GuideStatusService
    {
        private EndedToursService _endedToursService;
        private TourInstanceService _tourInstanceService;
        private TourReviewService _tourReviewService;
        public GuideStatusService() 
        {
            _endedToursService = new EndedToursService();
            _tourInstanceService = new TourInstanceService();
            _tourReviewService = new TourReviewService();
        }
        public int GetNumberOfCompletedTours()
        {
            List<TourInstance> tours = _tourInstanceService.GetEndedInstances();
            return tours.Count;
        }
        public StatusDTO GetGuideStatus(int year, string language/*, int guideId*/)
        {
            var tours = _endedToursService.FindEndedToursByYear(year)
                                          .Where(t => t.Language == language /*&& t.GuideId == guideId*/)
                                          .ToList();
            int completedTours = tours.Count;
            float averageScore = _tourReviewService.CalculateScoreByLanguage(language);

            return new StatusDTO
            {
                Year = year,
                Language = language,
                NumberOfCompletedTours = completedTours,
                AverageScore = averageScore
            };
        }
        public List<string> GetLanguages()
        {
            List<string> languages = new List<string>();
            List<TourReviewDTO> reviews = _tourReviewService.GetReviewDTOs();
            foreach (TourReviewDTO review in reviews)
            {
                Tour tour = _tourReviewService.GetTourByName(review.TourName);
                if (!languages.Contains(tour.Language))
                    languages.Add(tour.Language);
            }
            //languages.Add("Language");
            return languages;
        }

        public bool IsSuperGuide(int year)
        {
            List<string> languages = GetLanguages();
            foreach (var language in languages)
            {
                StatusDTO status = GetGuideStatus(year, language);
                if(status.NumberOfCompletedTours >= 20 && status.AverageScore > 4.0)
                {
                    return true; break;
                }
            }
            return false;
            //return status.NumberOfCompletedTours >= 20 && status.AverageScore > 4.0f;
        }
        public string GetSuperguideLanguage(int year)
        {
            List<string> languages = GetLanguages();
            string found = null;
            foreach (var language in languages)
            {
                StatusDTO status = GetGuideStatus(year, language);
                if (status.NumberOfCompletedTours >= 20 && status.AverageScore > 4.0)
                {
                    found = language;
                }
            }
            return found;
            //return status.NumberOfCompletedTours >= 20 && status.AverageScore > 4.0f;
        }
    }
    
}

