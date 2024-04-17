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


    }
}
