using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IOwnerNotificationRepository
    {
        public List<OwnerNotification> GetAll();
        public List<OwnerNotification> GetAllByOwnerId(int ownerId);
        public OwnerNotification Save(OwnerNotification ownerNotification);
        public int NextId();
        public void Delete(OwnerNotification ownerNotification);
        public OwnerNotification Update(OwnerNotification ownerNotification);
    }
}
