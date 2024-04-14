using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IKeyPointRepository
    {

        public List<KeyPoint> GetAll();

        public KeyPoint Save(KeyPoint keyPoint);

        public int NextId();

        public void Delete(KeyPoint keyPoint);
        public KeyPoint Update(KeyPoint keyPoint);
        public KeyPoint GetById(int id);
        public List<KeyPoint> GetByIdList(List<int> ids);

        public List<KeyPoint> GetKeypointsByIds(List<int> keypointIds);




    }
}
