using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace BookingApp.Service.TourServices
{
    public class TourService
    {
        private readonly ITourRepository iTourRepository;
       // private readonly KeyPointRepository KeyPointRepository;
       // private readonly ITourInstanceRepository iTourInstanceRepository;
        private TourInstanceService tourInstanceService;
        private PeopleInfoService peopleInfoService;
        private LocationService locationService;
        private KeyPointService keyPointService;
        public TourService()
        {
            iTourRepository = Injectorr.CreateInstance<ITourRepository>();
            tourInstanceService = new TourInstanceService();
            keyPointService = new KeyPointService();
            peopleInfoService = new PeopleInfoService();
            locationService = new LocationService();
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
        public int NextId() { return iTourRepository.NextId(); }

        public string GetTourNameById(int tourId)
        {
            return iTourRepository.GetTourNameById(tourId);
        }


        public List<Tour> GetToursByLocationId(int locationId)
        {
            return iTourRepository.GetToursByLocationId(locationId);
        }

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
        public Tour CreateTour(string name, string city, string country, string description, string language, int maxTourists, List<int> keyPointIds, List<DateTime> tourDates, int duration, List<string> imagePaths)
        {

            Location location = locationService.FindLocation(city, country);
            if (location == null)
            {
                location = new Location(city, country);
                locationService.Save(location);
            }

            if (keyPointIds.Count <= 2)
            {
                throw new ArgumentException("Tura mora da sadrži barem dve ključne tačke.");
            }

            Tour newTour = new Tour
            {
                Name = name,
                LocationId = location.Id,
                Description = description,
                Language = language,
                KeyPointIds = keyPointIds,
                Duration = duration,
                Images = imagePaths,
            };
            Save(newTour);
            //kreiranje instanci ture
            List<int> tourInstancesids = new List<int>();

            for (int i = 0; i < tourDates.Count; i++)
            {
                TourInstance tourInstance = new TourInstance(newTour.Id, maxTourists, 0, false, false, tourDates[i]);
                tourInstanceService.Save(tourInstance);
                int tourInstanceid = tourInstance.Id;
                tourInstancesids.Add(tourInstanceid);
            }
            keyPointService.SetKeyPointTourId(keyPointIds, newTour.Id);

            return newTour;
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
        public string LoadLocation(int locationId)
        {
            Location location = locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }

        public ActiveTourDTO getActiveTourDTO()
        {
            Tour tour = GetByActivity();
            KeyPoint activeKeyPoint = keyPointService.FindActiveKeyPoint(tour.KeyPointIds);
            ActiveTourDTO activeTour = new ActiveTourDTO
            {
                Name = tour.Name,
                //LocationId = tour.LocationId,
                Description = tour.Description,
                Language = tour.Language,
                //KeyPoints = tour.KeyPointIds,
                Duration = tour.Duration,
                //Images = tour.Images,
                //BitmapImages = tour.BitmapImages,
                Location = LoadLocation(tour.LocationId),
                ActiveKeyPoint = activeKeyPoint.Name
            };
            return activeTour;
        }


        public int FindPresentTouristsCount(int TourId)
        {
            Tour t = GetById(TourId);
            if (t == null)
                return 0;
            List<int> people = FindPresentTourists(t);
            int count = people.Count();
            return count;
        }

        public List<int> FindPresentTourists(Tour t)
        {
            List<int> people = new List<int>();
            foreach (int id in t.KeyPointIds)
            {
                KeyPoint kp = keyPointService.GetById(id); //i funkcija za prebrojavanje u servisu posebna.
                                                           //KeyPoint kp = KeyPointRepository.GetById(id);
                if (kp != null)
                {
                    foreach (int tourist in kp.PresentPeopleIds)
                    {
                        if (!people.Contains(tourist))
                            people.Add(tourist);
                    }
                }
            }
            return people;

        }
        public int CalculateNumberOfTouristsUnder18(Tour tour)
        {
            List<int> tourists = FindPresentTourists(tour);
            int count = 0;
            foreach (int id in tourists)
            {
                PeopleInfo tourist = peopleInfoService.GetById(id);
                if (tourist.Age < 18)
                    count++;
            }
            return count;
        }

        public int CalculateNumberOfTouristsMore50(Tour tour)
        {
            List<int> tourists = FindPresentTourists(tour);
            int count = 0;
            foreach (int id in tourists)
            {
                PeopleInfo tourist = peopleInfoService.GetById(id);
                if (tourist.Age > 50)
                    count++;
            }
            return count;
        }
        public int CalculateNumberOfTourists18And50(Tour tour)
        {
            List<int> tourists = FindPresentTourists(tour);
            int count = 0;
            foreach (int id in tourists)
            {
                PeopleInfo tourist = peopleInfoService.GetById(id);
                if (tourist.Age >= 18 && tourist.Age <= 50)
                    count++;
            }
            return count;
        }
        public float CalculateAttendacePercentage(TourInstance instance) 
        {
            Tour found = GetById(instance.IdTour);
            float count = (float)FindPresentTouristsCount(found.Id) / instance.ReservedTourists;
            return count * 100;
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



        public List<HomeTourDTO> GetAllTourDTOs() {

            List<Tour> tours = iTourRepository.GetAll();
            List<HomeTourDTO> homeTours= new List<HomeTourDTO>();

            foreach (Tour tour in tours)
            {

                Location location = locationService.GetById(tour.LocationId);
                string locationString = $" {location.Country}";

                

                HomeTourDTO homeTour = new HomeTourDTO(tour.Id,tour.Name,locationString, tour.Images);


                homeTours.Add(homeTour);

            }


            return homeTours;
        
        }




    }
}
