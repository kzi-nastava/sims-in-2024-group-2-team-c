using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class PeopleInfoRepository
    {

        private const string FilePath = "../../../Resources/Data/peoplesinfo.csv";
        private readonly Serializer<PeopleInfo> _serializer;
        private List<PeopleInfo> _peopleInfos;


        public PeopleInfoRepository()
        {
            _serializer = new Serializer<PeopleInfo>();
            _peopleInfos = _serializer.FromCSV(FilePath);
        }

        public List<PeopleInfo> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public PeopleInfo Save(PeopleInfo peopleInfo)
        {
            peopleInfo.Id = NextId();
            _peopleInfos = _serializer.FromCSV(FilePath);
            _peopleInfos.Add(peopleInfo);
            _serializer.ToCSV(FilePath, _peopleInfos);
            return peopleInfo;
        }

        public int NextId()
        {
            _peopleInfos = _serializer.FromCSV(FilePath);
            if (_peopleInfos.Count < 1)
            {
                return 1;
            }
            return _peopleInfos.Max(t => t.Id) + 1;
        }

        public void Delete(PeopleInfo peopleInfo)
        {
            _peopleInfos = _serializer.FromCSV(FilePath);
            PeopleInfo founded = _peopleInfos.Find(t => t.Id == peopleInfo.Id);
            _peopleInfos.Remove(founded);
            _serializer.ToCSV(FilePath, _peopleInfos);
        }

        public PeopleInfo Update(PeopleInfo peopleInfo)
        {
            _peopleInfos = _serializer.FromCSV(FilePath);
            PeopleInfo current = _peopleInfos.Find(t => t.Id == peopleInfo.Id);
            int index = _peopleInfos.IndexOf(current);
            _peopleInfos.Remove(current);
            _peopleInfos.Insert(index, peopleInfo);
            _serializer.ToCSV(FilePath, _peopleInfos);
            return peopleInfo;
        }


        public PeopleInfo GetById(int id)
        {
            _peopleInfos = _serializer.FromCSV(FilePath);
            return _peopleInfos.Find(c => c.Id == id);

        }



    }
}
