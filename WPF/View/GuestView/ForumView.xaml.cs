using BookingApp.WPF.ViewModel.GuestViewModel;
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

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for ForumView.xaml
    /// </summary>
    public partial class ForumView : Page
    {
        private readonly MainGuestWindow _mainGuestWindow;
        private readonly NavigationService _navigationService;

        public ForumView(MainGuestWindow mainGuestWindow, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new ForumViewModel(mainGuestWindow, navigationService);
            _mainGuestWindow = mainGuestWindow;
            _navigationService = navigationService;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (row != null && row.DataContext != null)
            {
                var viewModel = this.DataContext as ForumViewModel;
                if (viewModel != null && viewModel.ForumDoubleClickCommand.CanExecute(row.DataContext))
                {
                    viewModel.ForumDoubleClickCommand.Execute(row.DataContext);
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
