using GpsNotebook.Helpers;
using GpsNotebook.Model;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        private IPinLocationRepository _pinLocationRepository;
        private IAuthorization _authorization;
        private IPageDialogService _pageDialogService;
        public AddPinViewModel(
            INavigationService navigationService,
            IPinLocationRepository pinLocationRepository,
            IAuthorization authorization,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Add new pin";

            _pinLocationRepository = pinLocationRepository;
            _authorization = authorization;
            _pageDialogService = pageDialogService;
        }

        #region --  Public properties --

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

        private ICommand _savePin;
        public ICommand SavePin =>
            _savePin ?? (_savePin = new DelegateCommand(ExecuteSavePin, CanExecuteSavePin)
            .ObservesProperty<string>(() => PinName)
            .ObservesProperty<string>(() => PinDescription)
            .ObservesProperty<string>(() => PinLatitude)
            .ObservesProperty<string>(() => PinLongitude));

        public ICommand GetPosition => new Command<Position>(ExecuteGetPosition);

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

            //List<PinLocation> l = _pinLocationRepository.GetPinsLocation().ToList();

            await NavigationService.GoBackAsync();
        }

        private bool CanExecuteSavePin()
        {
            return FieldHelper.IsAllFieldsIsNullOrEmpty(PinName, PinDescription, PinLatitude, PinLongitude);
        }


        private void ExecuteGetPosition(Position obj)
        {
            PinLatitude = obj.Latitude.ToString();
            PinLongitude = obj.Longitude.ToString();
        }

        #endregion
    }
}
