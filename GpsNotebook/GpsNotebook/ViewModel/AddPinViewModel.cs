using GpsNotebook.Model;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        private IPinLocationRepository _pinLocationRepository;
        private IAuthorization _authorization;
        public AddPinViewModel(
            INavigationService navigationService,
            IPinLocationRepository pinLocationRepository,
            IAuthorization authorization) :
            base(navigationService)
        {
            Title = "Add new pin";

            _pinLocationRepository = pinLocationRepository;
            _authorization = authorization;
        }

        #region --  Public properties --

        private ICommand _savePin;
        public ICommand SavePin =>
            _savePin ?? (_savePin = new DelegateCommand(ExecuteSavePin));


        public ICommand GetPosition => new Command<Position>(ExecuteGetPosition);

        private void ExecuteGetPosition(Position obj)
        {
            PinLatitude = obj.Latitude.ToString();
            PinLongitude = obj.Longitude.ToString();
        }


        private async void ExecuteSavePin()
        {
            var pinLocation = new PinLocation
            {
                Description = PinDescription,
                Latitude = double.Parse(PinLatitude),
                Longitude = double.Parse(PinLongitude),
                PinName = PinName,
                UserId = _authorization.GetUserId()
            };

            _pinLocationRepository.AddPinLocation(pinLocation);

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
