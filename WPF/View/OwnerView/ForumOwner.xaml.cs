using System;
using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.OwnerViewModel;

namespace BookingApp.WPF.View.OwnerView
{
    public partial class ForumOwner : Page
    {
        private readonly OwnerForumViewModel _viewModel;

        public ForumOwner()
        {
            InitializeComponent();
            _viewModel = new OwnerForumViewModel();
            DataContext = _viewModel;
        }

        // Event handler za klik na forum
        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            // Implementiraj logiku za otvaranje detalja foruma
            // Možete koristiti sender kako biste dobili informacije o odabranom forumu
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddCommentButton_Click(sender, e);
        }
        private void Forum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is OwnerForumViewModel viewModel)
            {
                viewModel.Forum_SelectionChanged(sender, e);
            }
        }
    }
}
