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
        public Reservation Reservation { get; set; } //svuda smo reservation objekat
        //public string GuestUsername { get; set; } //dodato
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

        public AccommodationRate() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), Cleanliness.ToString(), OwnerRate.ToString(), Comment };
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
            Images = new List<string>();
            /*
            for (int i = 5; i < values.Length; i++)
            {
                Images.Add(values[i]);
            }
            */
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


    


