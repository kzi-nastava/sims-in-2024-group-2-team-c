using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IGuestNotificationRepository
    {
        public List<GuestNotification> GetAll();
        public List<GuestNotification> GetAllByGuestId(int guestId);
        public GuestNotification Save(GuestNotification guestNotification);
        public int NextId();
        public void Delete(GuestNotification guestNotification);
        public GuestNotification Update(GuestNotification guestNotification);
    }
}
