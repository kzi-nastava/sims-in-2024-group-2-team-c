using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class PeopleInfoService
    {
        public IPeopleInfoRepository _peopleInfoRepository;


        public PeopleInfoService()
        {
            _peopleInfoRepository = Injectorr.CreateInstance<IPeopleInfoRepository>();
        }
        public PeopleInfo GetById(int id)
        {
            return _peopleInfoRepository.GetById(id);
        }

        public PeopleInfo Save(PeopleInfo peopleInfo)
        {
            return _peopleInfoRepository.Save(peopleInfo);
        }
        public PeopleInfo Update(PeopleInfo peopleInfo)
        {
            return _peopleInfoRepository.Update(peopleInfo);
        }

        public List<int> SavePeopleInfoList(List<PeopleInfo> peopleInfoList)
        {
            List<int> savedIds = new List<int>();

            foreach (var person in peopleInfoList)
            {
                // Save each person using the repository and collect the returned ID
                PeopleInfo savedPerson = _peopleInfoRepository.Save(person);
                savedIds.Add(savedPerson.Id); // Assuming Id is the property that holds the ID
            }

            return savedIds;
        }
        public List<PeopleInfo> GetAll()
        {
            return _peopleInfoRepository.GetAll();
        }

    }
}
