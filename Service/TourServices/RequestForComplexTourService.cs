using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class RequestForComplexTourService
    {

        private readonly IRequestsForComplexTourRepository requestsForComplexTourRepository;

        public RequestForComplexTourService() {
            requestsForComplexTourRepository = Injectorr.CreateInstance<IRequestsForComplexTourRepository>();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
           return requestsForComplexTourRepository.Save(tourRequest);
        }


        public List<TourRequest> GetAll()
        {
            return requestsForComplexTourRepository.GetAll();
        }

        public DateTime GetTimeOfFirstRequest(int id)
        {
            List<TourRequest> tourRequests = GetAll();
            TourRequest request = tourRequests.FirstOrDefault(r => r.Id == id);
            return request.StartDate;
        }
        
        public bool AreAllRequestsOnHold(List<int> requestIds)
        {

            List<TourRequest> requests = GetAll();
            List<TourRequest> chosenRequests = new List<TourRequest>();
            TourRequest request = new TourRequest();
            foreach (int requestId in requestIds)
            {
                request = requests.FirstOrDefault(r => r.Id == requestId);
                chosenRequests.Add(request);
            }

            int counter = 0;
            foreach(TourRequest tourRequest in chosenRequests)
            {
                if(tourRequest.Status == TourRequestStatus.Accepted)
                {
                    counter++;
                }

            }


            if(counter == 0)
            {
                return true;
            }
            return false;

        }


         public bool CheckingStatusOfRequests(List<int> requestIds) { 
            List<TourRequest> tourRequests = GetAll();
            return requestIds.All(id => tourRequests.Any(request => request.Id == id && request.Status == TourRequestStatus.Accepted));
        }

    }
}
