﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    internal class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<string> KeyPoints { get; set; }
        public int Duration { get; set; }
        public List<string> Images { get; set; }

        public Tour() { }
        public Tour(int id, string name, Location location, string description, string language, List<string> keyPoints, int duration, List<string> images)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            //MaxTourists = maxTourists;
            KeyPoints = keyPoints;
            //Dates = dates;
            Duration = duration;
            Images = images;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            string[] locationValues = new string[2];
            locationValues[0] = values[2];
            locationValues[1] = values[3];
            Location.FromCSV(locationValues);
            Description = values[4];
            Language = values[5];
            //MaxTourists = Convert.ToInt32(values[6]);
            KeyPoints = values[6].Split(',').Select(point => point.Trim()).ToList();
            //Dates = values[8].Split(',').Select(date => DateTime.Parse(date.Trim())).ToList();
            Duration = Convert.ToInt32(values[7]);
            Images = values[8].Split(',').Select(image => image.Trim()).ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.City, Location.Country, Description, Language, /*MaxTourists.ToString(),*/ string.Join(", ", KeyPoints), /*string.Join(", ", Dates),*/ Duration.ToString(), string.Join(", ", Images) };
            return csvValues;
        }
    }
}
