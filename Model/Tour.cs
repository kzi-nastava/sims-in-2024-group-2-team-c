using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<int> KeyPointIds { get; set; }
        public int Duration { get; set; }
        public List<string> Images { get; set; }

        public Tour() {

            Name = string.Empty;
            Description = string.Empty;
            Language = string.Empty;
            KeyPointIds = new List<int>();
            Images = new List<string>();
        
        
        }
        public Tour(string name, int locationId, string description, string language, List<int> keyPointIds, int duration, List<string> images)
        {
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            KeyPointIds = keyPointIds;
            Duration = duration;
            Images = images;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            Language = values[4];
            /*List<string> keyPointIds = values[5].Split(',').ToList();
            foreach (string kp in keyPointIds)
            {
                int id = Convert.ToInt32(kp);
                KeyPointIds.Add(id);
            }*/
            KeyPointIds = values[5].Split(',').Select(int.Parse).ToList();
            Duration = Convert.ToInt32(values[6]);
            Images = values[7].Split(',').ToList();
           
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, Language, string.Join(",", KeyPointIds),  Duration.ToString(), string.Join(", ", Images), };
            return csvValues;
        }
    }
}
