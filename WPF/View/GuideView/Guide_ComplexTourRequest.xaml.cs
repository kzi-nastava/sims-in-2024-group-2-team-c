using BookingApp.WPF.ViewModel.GuideViewModel;
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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_ComplexTourRequest.xaml
    /// </summary>
    public partial class Guide_ComplexTourRequest : Page
    {
        public Guide_ComplexTourRequest()
        {
            InitializeComponent();
            DataContext = new ComplexTourRequest_ViewModel();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        /*private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.AcceptTourRequestCommand.Execute(requestsView.SelectedItem);
            var MainViewModel = DataContext as ComplexTourRequest_ViewModel;
            MainViewModel.Accept();

        }*/
        /* private void AcceptButton_Click(object sender, RoutedEventArgs e)
         {
             NavigationService?.GoBack();
         }*/

    }
}
