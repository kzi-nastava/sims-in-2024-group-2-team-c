using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class OwnerNotification : ISerializable
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public string Text { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString(), Text };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            Text = values[2];
        }

    }
}
