using GpsNotebook.Model;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class PinTabViewModel : ViewModelBase
    {
        public PinTabViewModel(INavigationService navigationPage) :
            base(navigationPage)
        {
            Title = "List pins";

            Pins = new ObservableCollection<PinLocation>()
            {
                new PinLocation() {Id = 1, UserId = 1, Description = "pin", Latitude = 50.47289, Longitude = 30.51358, PinName = "pin"}

            };
        }

        #region -- Public properties --

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
