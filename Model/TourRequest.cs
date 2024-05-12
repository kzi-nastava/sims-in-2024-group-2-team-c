﻿using BookingApp.Serializer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public enum TourRequestStatus { Accepted, Invalid, OnHold };
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public TourRequestStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationId { get; set; }
        public int GuideId { get; set; }
        public List<int> PeopleIds { get; set; }
        public string Language { get; set; }
        public int NumberOfPeople { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int TouristId;

        public TourRequest() { }
        public TourRequest( TourRequestStatus status, DateTime startDate, DateTime endDate, int locationId, int guideId, List<int> peopleIds, string language, int numberOfPeople, string description,DateTime dateOfCreation, int touristId)
        {
           
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            LocationId = locationId;
            GuideId = guideId;
            PeopleIds = peopleIds;
            Language = language;
            NumberOfPeople = numberOfPeople;
            Description = description;
            CreationDate = dateOfCreation;
            TouristId = touristId;
        }

        public string[] ToCSV()
        {

            string[] csvValues = { Id.ToString(), Status.ToString(), /*StartDate.ToString(), EndDate.ToString(),*/ StartDate.ToString("dd.MM.yyyy. HH:mm:ss"),EndDate.ToString("dd.MM.yyyy. HH:mm:ss"), LocationId.ToString(),
                                  GuideId.ToString(), string.Join(",", PeopleIds), Language, NumberOfPeople.ToString(), Description,CreationDate.ToString("dd.MM.yyyy. HH:mm:ss"), TouristId.ToString()  };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Status = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[1]);
            //StartDate = Convert.ToDateTime(values[2]);
            // EndDate = Convert.ToDateTime(values[3]);
            StartDate = DateTime.ParseExact(values[2].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(values[3].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            LocationId = Convert.ToInt32(values[4]);
            GuideId= Convert.ToInt32(values[5]);
            PeopleIds = values[6].Split(',').Select(int.Parse).ToList();
            Language = values[7];
            NumberOfPeople = Convert.ToInt32(values[8]);
            Description = values[9];
            CreationDate = DateTime.ParseExact(values[10].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            TouristId = Convert.ToInt32(values[11]);

        }
    }
}
