using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class GuideRepository : IGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guide.csv";

        private readonly Serializer<Guide> _serializer;

        private List<Guide> _guide;
        public GuideRepository() 
        {
            _serializer = new Serializer<Guide>();
            _guide = _serializer.FromCSV(FilePath);
        }
        public List<Guide> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guide Save(Guide guide)
        {
            guide.Id = NextId();
            _guide = _serializer.FromCSV(FilePath);
            _guide.Add(guide);
            _serializer.ToCSV(FilePath, _guide);
            return guide;
        }

        public int NextId()
        {
            _guide = _serializer.FromCSV(FilePath);
            if (_guide.Count < 1)
            {
                return 1;
            }
            return _guide.Max(c => c.Id) + 1;
        }

        public void Delete(Guide guide)
        {
            _guide = _serializer.FromCSV(FilePath);
            Guide founded = _guide.Find(c => c.Id == guide.Id);
            _guide.Remove(founded);
            _serializer.ToCSV(FilePath, _guide);
        }

        public Guide Update(Guide guide)
        {
            _guide = _serializer.FromCSV(FilePath);
            Guide current = _guide.Find(c => c.Id == guide.Id);
            int index = _guide.IndexOf(current);
            _guide.Remove(current);
            _guide.Insert(index, guide);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _guide);
            return guide;
        }
        public Guide GetById(int id)
        {
            _guide = _serializer.FromCSV(FilePath);
            return _guide.Find(c => c.Id == id);

        }
    }

}
