using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }

        public Location Location { get; set; }

        public User User { get; set; }

        public bool IsForumMine { get; set; }

        public String ForumComment { get; set; }

        public bool HasBeenVisited { get; set; }

        public bool IsForumClosed { get; set; }


        public Forum() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Location.Id.ToString(), User.Id.ToString(), IsForumMine.ToString(), ForumComment, 
                HasBeenVisited.ToString(), IsForumClosed.ToString()};
            
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            User = new User() { Id = Convert.ToInt32(values[2]) };
            IsForumMine = Convert.ToBoolean(values[3]);
            ForumComment = values[4];
            HasBeenVisited = Convert.ToBoolean(values[5]);
            IsForumClosed = Convert.ToBoolean(values[6]);
        }


    }
}
