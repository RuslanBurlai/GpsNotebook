using GpsNotebook.Model;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class PinViewModel : ViewModelBase
    {
        public PinViewModel(INavigationService navigationPage) :
            base(navigationPage)
        {
            Title = "List pins";

            Pins = new ObservableCollection<PinLocation>()
            {
                new PinLocation()
            };
        }

        #region -- Public properties --

        private CustomPin _pin;
        public CustomPin Pin
        {
            get { return _pin; }
            set { SetProperty(ref _pin, value); }
        }

        private ObservableCollection<PinLocation> _pins;
        public ObservableCollection<PinLocation> Pins
        {
            get { return _pins; }
            set { SetProperty(ref _pins, value); }
        }

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }


        #endregion
    }
}
