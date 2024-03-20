using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class OwnerRepository
    {

        private const string FilePath = "../../../Resources/Data/owners.csv";
        private readonly Serializer<Owner> _serializer;
        public int NextId()
        {
            List<Owner> owners = _serializer.FromCSV(FilePath);
            if (owners.Count < 1)
            {
                return 1;
            }
            return owners.Max(guest => guest.Id) + 1;
        }

    }
}