using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Service
{
    public class NavigationService
    {

        private readonly Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        }



        public void NavigateToPage(Type pageType)
        {
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            _frame.Navigate(Activator.CreateInstance(pageType));
        }


        public void NavigateToPage(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            _frame.Navigate(page);
        }

        public void NavigateToPage(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            _frame.Navigate(uri);
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }

        public void GoForward()
        {
            if (_frame.CanGoForward)
            {
                _frame.GoForward();
            }
        }

    }
}
