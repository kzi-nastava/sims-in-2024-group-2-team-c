using BookingApp.DTO;
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
        public void SetKeyPointTourId(List<int> keyPointIds, int tourId)
        {
            foreach (int id in keyPointIds)
            {
                KeyPoint kp = _keyPointRepository.GetById(id);
                kp.TourId = tourId;
                _keyPointRepository.Update(kp);
            }
        }
        public List<int> ParseKeyPointIds(List<string> keyPointsList)
        {
            List<int> ids = new List<int>();
            KeyPoint startedPoint = SaveKeyPoint(keyPointsList[0], true, false);
            ids.Add(startedPoint.Id);

            for (int i = 1; i < keyPointsList.Count - 1; i++)
            {
                KeyPoint kp = SaveKeyPoint(keyPointsList[i], false, false);
                ids.Add(kp.Id);
            }
            KeyPoint endedPoint = SaveKeyPoint(keyPointsList[keyPointsList.Count - 1], false, true);
            ids.Add(endedPoint.Id);
            return ids;
        }
        public KeyPoint SaveKeyPoint(string Name, bool startPoint, bool endPoint)
        {
            KeyPoint kp = new KeyPoint
            {
                Name = Name,
                StartingPoint = startPoint,
                EndingPoint = endPoint
            };
            _keyPointRepository.Save(kp);
            return kp;
        }

    }
}
