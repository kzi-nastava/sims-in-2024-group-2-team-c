using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TouristView
{
    /// <summary>
    /// Interaction logic for SavedReviewView.xaml
    /// </summary>
    public partial class SavedReviewView : Page
    {

        private MainViewModel mainViewModel;
        public SavedReviewView()
        {
            InitializeComponent();
            DataContext = new SavedReviewViewModel();
        }

       
    }
}
