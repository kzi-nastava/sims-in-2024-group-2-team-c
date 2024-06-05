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
    public class ForumRepository : IForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forum> _serializer;
        private List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
        }

        public Forum GetById(int id)
        {
            return _forums.FirstOrDefault(f => f.Id == id);
        }

        public Forum Save(Forum forum)
        {
            forum.Id = NextId();
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }

        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(a => a.Id) + 1;
        }

        public void Delete(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum founded = _forums.Find(a => a.Id == forum.Id);
            _forums.Remove(founded);
            _serializer.ToCSV(FilePath, _forums);
        }

        /*
        public void Update(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum current = _forums.Find(a => a.Id == forum.Id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);
            _serializer.ToCSV(FilePath, _forums);
        }
        */

        public void Update(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);

            int index = _forums.FindIndex(r => r.Id == forum.Id);
            if (index != -1)
            {
                _forums[index] = forum;
                _serializer.ToCSV(FilePath, _forums);
            }
            else
            {
                throw new Exception("Forum with the specified ID not found.");
            }
        }


        public List<Forum> GetAll()
        {
            return _forums;
        }
    }
}
