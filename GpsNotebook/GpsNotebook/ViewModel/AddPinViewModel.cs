using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        public AddPinViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Add new pin";
        }

        #region --  Public properties --

        private ICommand _savePin;
        public ICommand SavePin =>
            _savePin ?? (_savePin = new DelegateCommand(ExecuteSavePin));

        private async void ExecuteSavePin()
        {
            await NavigationService.GoBackAsync();
        }

        private string _pinName;
        public string PinName
        {
            get { return _pinName; }
            set { SetProperty(ref _pinName, value); }
        }

        private string _pinDescription;
        public string PinDescription
        {
            get { return _pinDescription; }
            set { SetProperty(ref _pinDescription, value); }
        }

        private string _pinLatitude;
        public string PinLatitude
        {
            get { return _pinLatitude; }
            set { SetProperty(ref _pinLatitude, value); }
        }

        private string _pinLongitude;
        public string PinLongitude
        {
            get { return _pinLongitude; }
            set { SetProperty(ref _pinLongitude, value); }
        }

        #endregion
    }
}
