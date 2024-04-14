using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class KeyPoint : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool StartingPoint { get; set; }
        public bool EndingPoint { get; set; }
        public List<int> PeopleIds { get; set; }
        public List<int> PresentPeopleIds { get; set; }
        public int TourId { get; set; }

        public KeyPoint()
        {
            Name = string.Empty;
            Description = string.Empty;
            PeopleIds = new List<int>();
            PresentPeopleIds = new List<int>();
        }
        public KeyPoint(string name, string description, bool startingPoint, bool endingPoint, int tourId)
        {
            Name = name;
            Description = description;
            Active = false;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            PeopleIds = new List<int>();
            PresentPeopleIds = new List<int>();
            TourId = tourId;
        }

        public KeyPoint(string name, string description, bool startingPoint, bool endingPoint, List<int> peopleIds, List<int> presentPeopleIds, int tourId)
        {
            Name = name;
            Description = description;
            Active = false;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            PeopleIds = peopleIds;
            PresentPeopleIds = presentPeopleIds;
            //TouristsId = new List<int>();
            TourId = tourId;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            Active = bool.Parse(values[3]);
            StartingPoint = bool.Parse(values[4]);
            EndingPoint = bool.Parse(values[5]);
            if (!string.IsNullOrEmpty(values[6]))
            {
                PeopleIds = values[6].Split(',').Select(int.Parse).ToList();
            }
            else
            {
                PeopleIds = new List<int>();
            }
            if (!string.IsNullOrEmpty(values[7]))
            {
                PresentPeopleIds = values[7].Split(',').Select(int.Parse).ToList();
            }
            else
            {
                PresentPeopleIds = new List<int>();
            }
            TourId = Convert.ToInt32(values[8]);

        }

        public string[] ToCSV()
        {

            string[] csvValues = { Id.ToString(), Name, Description, Active.ToString(), StartingPoint.ToString(), EndingPoint.ToString(), string.Join(",", PeopleIds), string.Join(",", PresentPeopleIds), TourId.ToString() };
            return csvValues;
        }
    }
}