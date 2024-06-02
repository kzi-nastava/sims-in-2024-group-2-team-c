using System;
using System.ComponentModel;
using System.Windows;

namespace BookingApp
{
    public partial class App : Application, INotifyPropertyChanged
    {
        private static string currentLanguage = "\\Resources\\ResourcesLan.xaml";

        public static string CurrentLanguage
        {
            get => currentLanguage;
            set
            {
                if (currentLanguage != value)
                {
                    currentLanguage = value;
                    OnPropertyChangedStatic(nameof(CurrentLanguage));
                    UpdateResources();
                }
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnPropertyChangedStatic(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        private static void UpdateResources()
        {
            var newResource = new ResourceDictionary
            {
                Source = new Uri(CurrentLanguage, UriKind.Relative)
            };

            //Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newResource);

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Content is FrameworkElement frameworkElement)
                {
                    frameworkElement.Resources.MergedDictionaries.Clear();
                    frameworkElement.Resources.MergedDictionaries.Add(newResource);
                }
            }
        }

        public static void ChangeLanguage(string resourcePath)
        {
            CurrentLanguage = resourcePath;
        }

        public static void ChangeTheme(string resourcePath)
        {
            var newResource = new ResourceDictionary
            {
                Source = new Uri(resourcePath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(newResource);

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Content is FrameworkElement frameworkElement)
                {
                    frameworkElement.Resources.MergedDictionaries.Clear();
                    frameworkElement.Resources.MergedDictionaries.Add(newResource);
                }
            }

            MessageBox.Show("Theme changed successfully.");
        }

        // Implementacija interfejsa INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
