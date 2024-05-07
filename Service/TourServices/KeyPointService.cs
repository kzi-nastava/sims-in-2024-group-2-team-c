using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Service.TourServices
{
    public  class KeyPointService
    {

        public IKeyPointRepository _keyPointRepository { get; set; }
        public ITourRepository _tourRepository { get; set; }
        

        public KeyPointService() {
            _keyPointRepository = Injectorr.CreateInstance<IKeyPointRepository>();
            _tourRepository = new TourRepository();  
        }

        public List<KeyPoint> GetKeyPointsByTourId(int tourId)
        { 
            
            

            Tour tour = _tourRepository.GetById(tourId);
            

            if (tour == null)
            {
                
                return new List<KeyPoint>();
            }


            List<int> keyPointIds = tour.KeyPointIds;

            List<KeyPoint> keyPoints = _keyPointRepository.GetAll();

            List<KeyPoint> keyPointsForTour = keyPoints
                .Where(kp => keyPointIds.Contains(kp.Id))
                .ToList();

            return keyPointsForTour;

        }
        public KeyPoint GetById(int id)
        {
            return _keyPointRepository.GetById(id);
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            return _keyPointRepository.Update(keyPoint);
        }

        public List<KeyPoint> GetKeypointsByIds(List<int> keypointIds)
        {
            return _keyPointRepository.GetKeypointsByIds(keypointIds);
        }
        public KeyPoint FindActiveKeyPoint(List<int> ids)
        {
            KeyPoint kp = null;
            foreach (int id in ids)
            {
                kp = GetById(id);
                if (kp.Active == true)
                    return kp;
            }
            return kp;
        }

    }
}
