using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ILanguageRepository
    {

        public List<Language> GetAll();



        public Language Save(Language language);


        public int NextId();

        public void Delete(Language language);
        public Language Update(Language language);

        public Language GetById(int id);


    }
}
