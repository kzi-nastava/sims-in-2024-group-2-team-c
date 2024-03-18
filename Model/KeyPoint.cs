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
        public string Description {  get; set; }
        public bool Active {  get; set; }
        public bool StartingPoint { get; set; }
        public bool EndingPoint {  get; set; }

        //public List<int> TouristsId { get; set; }

        public KeyPoint() {
            Name = string.Empty;
            Description = string.Empty;
            //TouristsId = new List<int>();
        
        }

        public KeyPoint(string name, string description, bool startingPoint, bool endingPoint/*,List<int> touristIds*/)
        {
            Name = name;
            Description = description;
            Active = false;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            //TouristsId = touristIds;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            Active = bool.Parse(values[3]);
            StartingPoint = bool.Parse(values[4]);
            EndingPoint = bool.Parse(values[5]);
            //TouristsId = values[6].Split(',').Select(int.Parse).ToList();


        }

        public string[] ToCSV()
        {
            
            string[] csvValues = { Id.ToString(), Name, Description, Active.ToString(), StartingPoint.ToString(), EndingPoint.ToString()/*, string.Join(",", TouristsId)*/ };
            return csvValues;
        }
    }
}
