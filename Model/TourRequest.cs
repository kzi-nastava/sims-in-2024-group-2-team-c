using BookingApp.Serializer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationId { get; set; }
        public int GuideId { get; set; }
        public List<int> PeopleIds { get; set; }
        public string Language { get; set; }
        public int NumberOfPeople { get; set; }
        public string Description { get; set; }

        public TourRequest() { }
        public TourRequest(int id, bool status, DateTime startDate, DateTime endDate, int locationId, int guideId, List<int> peopleIds, string language, int numberOfPeople, string description)
        {
            Id = id;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            LocationId = locationId;
            GuideId = guideId;
            PeopleIds = peopleIds;
            Language = language;
            NumberOfPeople = numberOfPeople;
            Description = description;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Status.ToString(), StartDate.ToString(), EndDate.ToString(), LocationId.ToString(),
                                  GuideId.ToString(), string.Join(",", PeopleIds), Language, NumberOfPeople.ToString(), Description  };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Status = Convert.ToBoolean(values[1]);
            StartDate = Convert.ToDateTime(values[2]);
            EndDate = Convert.ToDateTime(values[3]);
            LocationId = Convert.ToInt32(values[4]);
            GuideId= Convert.ToInt32(values[5]);
            PeopleIds = values[5].Split(',').Select(int.Parse).ToList();
            Language = values[6];
            NumberOfPeople = Convert.ToInt32(values[7]);
            Description = values[8];
        }
    }
}
