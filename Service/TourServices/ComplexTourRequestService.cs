﻿using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class ComplexTourRequestService
    {

        private readonly IComplexTourRequestRepository complexTourRequestRepository;
        private readonly PeopleInfoService peopleInfoService;
        private readonly RequestForComplexTourService requestForComplexTourService;
        

        public ComplexTourRequestService() {
            complexTourRequestRepository = Injectorr.CreateInstance<IComplexTourRequestRepository>();
            peopleInfoService = new PeopleInfoService();
            requestForComplexTourService = new RequestForComplexTourService();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return complexTourRequestRepository.GetAll();
        }

        public ComplexTourRequest Save(ComplexTourRequest request)
        {
            return complexTourRequestRepository.Save(request);
        }

        public ComplexTourRequest Update(ComplexTourRequest request)
        {
            return complexTourRequestRepository.Update(request);
        }

        public int CreateTourRequest(Location location, string description, Language language, DateTime startDate, DateTime endDate, List<PeopleInfo> peopleInfos)
        {

            List<int> peopleinfoIds = peopleInfoService.SavePeopleInfoList(peopleInfos);

            TourRequest tourRequest = new TourRequest(TourRequestStatus.OnHold, startDate, endDate, location.Id, 3, peopleinfoIds, language.Name, peopleInfos.Count(), description, DateTime.Now, LoggedInUser.Id);
            requestForComplexTourService.Save(tourRequest);

            return tourRequest.Id;
       }

        public void CreateComplexTour(List<PartViewModel> parts)
        {
            List<int> requestIds = new List<int>();

            foreach (PartViewModel part in parts)
            {
                requestIds.Add(part.RequestId);
            }

            ComplexTourRequest tourRequest = new ComplexTourRequest(requestIds,LoggedInUser.Id,ComplexTourRequestStatus.OnHold);

            Save(tourRequest);

        }

        public List<TouristRequestDTO> GetTouristRequests()
        {

            List<ComplexTourRequest> requests = GetAll();

            List<ComplexTourRequest> touristsRequests = requests.Where(request => request.TouristId == LoggedInUser.Id).ToList();

            List<TouristRequestDTO> requestDtos = new List<TouristRequestDTO>();

            int number = 0;

            foreach (ComplexTourRequest request in touristsRequests)
            {

                TouristRequestDTO touristRequestDTO;
                if (request.Status == ComplexTourRequestStatus.Accepted)
                {
                    touristRequestDTO = new TouristRequestDTO("ACCEPTED", ++number, request.Id);
                }
                else if (request.Status == ComplexTourRequestStatus.Invalid)
                {
                    touristRequestDTO = new TouristRequestDTO("INVALID", ++number, request.Id);
                }
                else
                {
                    touristRequestDTO = new TouristRequestDTO("ON HOLD", ++number, request.Id);
                }
                requestDtos.Add(touristRequestDTO);


            }


            return requestDtos;
        }




    }
}
