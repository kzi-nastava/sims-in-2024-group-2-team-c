using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class ViewModelCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Predicate<T> _canExecuteAction;
        private Func<AccommodationRate> rateAccommodation;
        private Action navigateToRenovatePage;

        public ViewModelCommand(Action<T> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public ViewModelCommand(Func<AccommodationRate> rateAccommodation)
        {
            this.rateAccommodation = rateAccommodation;
        }

        public ViewModelCommand(Action navigateToRenovatePage)
        {
            this.navigateToRenovatePage = navigateToRenovatePage;
        }

        public ViewModelCommand(Action<T> executeAction, Predicate<T> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null || _canExecuteAction((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }
    }
}
