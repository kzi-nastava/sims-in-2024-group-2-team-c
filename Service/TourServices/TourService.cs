using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    internal class TourService
    {
        private readonly ITourRepository iTourRepository;
        private readonly KeyPointRepository KeyPointRepository; //SERVICEEEE
        private readonly ITourInstanceRepository iTourInstanceRepository;
        private TourInstanceService tourInstanceService;
        //private LocationService LocationService;
        //private KeyPointService keyPointService;
        public TourService(ITourRepository iTourRepository)
        {
            this.iTourRepository = iTourRepository;
            tourInstanceService = new(new TourInstanceRepository());
            //tourLocationService = new(new LocationRepository());
            //tourReservationService = new(new TourReservationRepository());
        }
        /*public Tour GetTourLocation(string country, string city)
        {
            return LocationService.GetTourLocationByCountryAndCity(country, city);
        }*/
        public void Save(Tour tour)
        {
            iTourRepository.Save(tour);
        }
        public void Delete(Tour tour) {  iTourRepository.Delete(tour); }

        public List<Tour> GetAll() {  return iTourRepository.GetAll(); }
        public Tour GetById(int id) { return iTourRepository.GetById(id); }

        public List<Tour> GetFutureTours()
        {
            List<TourInstance> instances = tourInstanceService.GetFutureInstance();
            List<Tour> founded = new List<Tour>();
            foreach (TourInstance instance in instances)
            {
                Tour t = GetById(instance.Id);
                if (!founded.Contains(t))
                {
                    founded.Add(t);
                }
            }
            return founded;
        }
       public Tour GetByActivity()
        {
            List<TourInstance> instances = tourInstanceService.GetAll();
            foreach (TourInstance i in instances)
            {
                if(i.Started == true && i.Ended == false)
                {
                    Tour founded = GetById(i.Id);
                    return founded;
                }
            }
            return null;
        }
        /*public int FindPresentTouristsCount(int TourId)
        {
            Tour t = GetById(TourId);
            List<int> people = new List<int>();
            int count = 0;
            foreach (int id in t.KeyPointIds)
            {
                //KeyPoint kp = KeyPointService.GetById(id); i funkcija za prebrojavanje u servisu posebna.
                KeyPoint kp = KeyPointRepository.GetById(id);
                if (kp == null) return 0;
                foreach (int tourist in kp.PresentPeopleIds)
                {
                    if (!people.Contains(tourist))
                        people.Add(tourist);
                }
            }
            count = people.Count();
            return count;
        }*/
        /*public void SaveKeyPoint(KeyPoint kp)
        {
            KeyPointService.Save(kp);
        }*/








    }
}
