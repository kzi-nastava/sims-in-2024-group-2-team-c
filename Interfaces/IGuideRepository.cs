using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IGuideRepository
    {
        public List<Guide> GetAll();

        public Guide Save(Guide guide);

        public int NextId();

        public void Delete(Guide guide);
        public Guide Update(Guide guide);
        public Guide GetById(int id);
        public List<Guide> GetByIdList(List<int> ids);

        public List<Guide> GetGuidesByIds(List<int> guideIds);
    }
}
