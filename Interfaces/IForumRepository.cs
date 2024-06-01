using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IForumRepository
    {

        public Forum Save(Forum forum);
        public int NextId();
        public void Delete(Forum forum);
        public void Update(Forum forum);
        List<Forum> GetAll();
        Forum GetById(int forumId);
    }

}
