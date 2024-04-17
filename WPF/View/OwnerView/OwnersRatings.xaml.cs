using BookingApp.Model;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnersRatings.xaml
    /// </summary>
    public partial class OwnersRatings : Page, INotifyPropertyChanged
    {

        private OwnersRatingsViewModel viewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public OwnersRatings()
        {
            InitializeComponent();
            viewModel = new OwnersRatingsViewModel();
            DataContext = viewModel;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
