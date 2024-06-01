using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IComplexTourRequestRepository
    {


        public List<ComplexTourRequest> GetAll();

        public ComplexTourRequest Save(ComplexTourRequest request);

        public int NextId();

        public void Delete(ComplexTourRequest request);
        public ComplexTourRequest Update(ComplexTourRequest request);

        

    }
}
