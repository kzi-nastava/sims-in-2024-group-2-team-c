﻿using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D.Converters;

namespace BookingApp.Service.TourServices
{
    public class TourReccommendationsService
    {

        private readonly ITourReccommendationsRepository tourReccommendationsRepository;
        private readonly TourRequestService tourRequestService;
        private readonly LocationService locationService;

        public TourReccommendationsService() {
            tourReccommendationsRepository = Injectorr.CreateInstance<ITourReccommendationsRepository>();
            tourRequestService = new TourRequestService();
            locationService = new LocationService();
        }

        public void Save(TourReccommendations reccommendations)
        {
            tourReccommendationsRepository.Save(reccommendations);
        }

        public List<TourReccommendations> GetAll()
        {
            return tourReccommendationsRepository.GetAll();
        }

        public List<TourReccommendations> MakeReccommendations() {

            List<TourReccommendations> recommendations = GetAll();

            // Retrieve all requests
            List<TourRequest> requests = tourRequestService.GetAll();

            // Filter accepted requests
            List<TourRequest> acceptedRequests = requests
                .Where(request => request.TouristId != LoggedInUser.Id &&
                                  request.Status == TourRequestStatus.Accepted &&
                                  !recommendations.Any(recommendation => recommendation.RequestId == request.Id)).ToList();

            List<TourRequest> touristRequests = requests.Where(request => request.TouristId == LoggedInUser.Id && request.Status == TourRequestStatus.Invalid).ToList();

           foreach (TourRequest request in acceptedRequests)
           {

                foreach(TourRequest tourRequest in touristRequests)
                {
                    Location RequestLocation = locationService.GetById(request.LocationId);
                    Location TouristLocation = locationService.GetById(tourRequest.LocationId);
                    SeeConditions(recommendations, request, tourRequest, RequestLocation, TouristLocation);

                }
            }
           return recommendations;
        }

        private void SeeConditions(List<TourReccommendations> recommendations, TourRequest request, TourRequest tourRequest, Location RequestLocation, Location TouristLocation)
        {
            bool isLanguage = false;
            bool isLocation = false;

            if (tourRequest.Language == request.Language && (RequestLocation.City != TouristLocation.City || RequestLocation.Country != TouristLocation.Country))
            {
                isLanguage = true; 
                CreateRecommendation(recommendations, request, RequestLocation, isLanguage,isLocation);
            }
            else if (tourRequest.Language != request.Language && (RequestLocation.City == TouristLocation.City || RequestLocation.Country == TouristLocation.Country))
            {
                isLocation = true;
                CreateRecommendation(recommendations, request, RequestLocation, isLanguage, isLocation);
            }
            else if (tourRequest.Language == request.Language && (RequestLocation.City == TouristLocation.City || RequestLocation.Country == TouristLocation.Country))
            {
                isLanguage = true; isLocation = true;
                CreateRecommendation(recommendations, request, RequestLocation, isLanguage, isLocation);
            }
        }

        private void CreateRecommendation(List<TourReccommendations> recommendations, TourRequest request, Location RequestLocation, bool isLanguage, bool isLocation)
        {
            TourReccommendations tourReccommendation = new TourReccommendations(12, isLanguage, isLocation, $"{RequestLocation.City}, {RequestLocation.Country}", request.Language, "guide1", request.Id);
            tourReccommendationsRepository.Save(tourReccommendation);
            recommendations.Add(tourReccommendation);
        }
    }
}
