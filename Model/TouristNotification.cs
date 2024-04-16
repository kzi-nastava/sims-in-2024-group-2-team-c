using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace BookingApp.Model
{
    public class TouristNotification : ISerializable
    {

        public int Id { get; set; }
        public string Name {  get; set; }

        public int TourInstanceId {  get; set; }

        public string TourName { get; set;}

        public int TouristId { get; set; }


        public TouristNotification() {
            Name = string.Empty;
            TourName = string.Empty;
        }

        public TouristNotification( string name, string tourName, int touristId, int tourInstanceId)
        {
            Name = name;
            TourName = tourName;
            TouristId = touristId;
            TourInstanceId = tourInstanceId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, TourName, TouristId.ToString(),TourInstanceId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourName = values[2];
            TouristId = Convert.ToInt32(values[3]);
            TourInstanceId= Convert.ToInt32(values[4]);
        }
    }
}
