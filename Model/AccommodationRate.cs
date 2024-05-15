using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using System.ComponentModel;

namespace BookingApp.Model
{
    public class AccommodationRate : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Reservation Reservation { get; set; }

        private string _guestUsername;
        public string GuestUsername
        {
            get { return _guestUsername; }
            set
            {
                _guestUsername = value;
                OnPropertyChanged("GuestUsername");
            }
        }

        public int Cleanliness { get; set; }
        public int OwnerRate { get; set; }
        public string Comment { get; set; }
        public List<String> Images { get; set; }

        public string WhatToRenovate { get; set; }

        public int LevelOfUrgency { get; set; }

        public DateTime AccommodationRatingTime { get; set; }

        public AccommodationRate() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), Cleanliness.ToString(), OwnerRate.ToString(), Comment, WhatToRenovate, LevelOfUrgency.ToString(), AccommodationRatingTime.ToString()};

            csvValues = csvValues.Concat(Images).ToArray();
            /*
            foreach (string imagePath in Images)
            {
                csvValues.Append($"{imagePath};");
            }
            */

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new Reservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            OwnerRate = Convert.ToInt32(values[3]);
            Comment = values[4];
            //Images = new List<string>();
            WhatToRenovate = values[5];
            LevelOfUrgency = Convert.ToInt32(values[6]);
            AccommodationRatingTime = Convert.ToDateTime(values[7]);
            /*
            for (int i = 5; i < values.Length; i++)
            {
                Images.Add(values[i]);
            }
            */
            Images = values.Skip(8).ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


    


