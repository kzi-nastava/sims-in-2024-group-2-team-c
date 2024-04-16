using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IPeopleInfoRepository
    {

        public List<PeopleInfo> GetAll();

        public PeopleInfo Save(PeopleInfo peopleInfo);

        public int NextId();

        public void Delete(PeopleInfo peopleInfo);

        public PeopleInfo Update(PeopleInfo peopleInfo);


        public PeopleInfo GetById(int id);


    }
}
