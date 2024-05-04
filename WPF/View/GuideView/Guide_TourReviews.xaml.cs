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
    /// Interaction logic for Guide_TourReviews.xaml
    /// </summary>
    public partial class Guide_TourReviews : Page
    {
        private readonly TourReview_ViewModel viewModel;

        public Guide_TourReviews()
        {
            InitializeComponent();
            viewModel = new TourReview_ViewModel();
            DataContext = viewModel;
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ReportReviewCommand.Execute(reviewsList.SelectedItem);
            //viewModel.UpdateVisibility();

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
