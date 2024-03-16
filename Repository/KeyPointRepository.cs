using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BookingApp.Repository
{
    internal class KeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private List<KeyPoint> _keypoint;

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _keypoint = _serializer.FromCSV(FilePath);
        }

        public List<KeyPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keypoint = _serializer.FromCSV(FilePath);
            _keypoint.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keypoint);
            return keyPoint;
        }

        public int NextId()
        {
            _keypoint = _serializer.FromCSV(FilePath);
            if (_keypoint.Count < 1)
            {
                return 1;
            }
            return _keypoint.Max(c => c.Id) + 1;
        }

        public void Delete(KeyPoint keyPoint)
        {
            _keypoint = _serializer.FromCSV(FilePath);
            KeyPoint founded = _keypoint.Find(c => c.Id == keyPoint.Id);
            _keypoint.Remove(founded);
            _serializer.ToCSV(FilePath, _keypoint);
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            _keypoint = _serializer.FromCSV(FilePath);
            KeyPoint current = _keypoint.Find(c => c.Id == keyPoint.Id);
            int index = _keypoint.IndexOf(current);
            _keypoint.Remove(current);
            _keypoint.Insert(index, keyPoint);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _keypoint);
            return keyPoint;
        }





    }
}
