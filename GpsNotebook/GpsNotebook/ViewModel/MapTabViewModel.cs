using GpsNotebook.Services.PinLocationRepository;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using GpsNotebook.Extensions;
using GpsNotebook.Models;
using System.ComponentModel;
using Prism.Services.Dialogs;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Prism.Commands;
using GpsNotebook.Services.Authorization;
using GpsNotebook.View;
using Newtonsoft.Json;
using GpsNotebook.Dialogs;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;

namespace GpsNotebook.ViewModel
{
    public class MapTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IDialogService _dialogService;
        private IAuthorizationService _authorizationService;

        public MapTabViewModel(
            INavigationService navigationService,
            IPinModelService pinModelService,
            IDialogService dialogService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "Map with pins";

            _pinModelService = pinModelService;
            _dialogService = dialogService;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private ObservableCollection<Pin> _allPins;
        public ObservableCollection<Pin> AllPins
        {
            get { return _allPins; }
            set { SetProperty(ref _allPins, value); }
        }

        private Pin _tapOnPin;
        public Pin TapOnPin
        {
            get { return _tapOnPin; }
            set { SetProperty(ref _tapOnPin, value); }
        }

        private ICommand _logOut;
        public ICommand LogOut =>
            _logOut ?? (_logOut = new DelegateCommand(OnLogOutCommand));

        private ICommand searchPins;
        public ICommand SearchPins =>
            searchPins ?? (searchPins = new Command<string>(OnGetSearchPins));

        private MapSpan _moveCameraToPin;
        public MapSpan MoveCameraToPin
        {
            get { return _moveCameraToPin; }
            set { SetProperty(ref _moveCameraToPin, value); }
        }

        private ICommand _navigateToScanQrCode;
        public ICommand NavigateToScanQrCode =>
            _navigateToScanQrCode ?? (_navigateToScanQrCode = new DelegateCommand(OnNavigateToScanQrCode));


        #endregion

        #region -- Private Helpers --

        private async void OnLogOutCommand()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(SignInView)}");
        }

        private void OnGetSearchPins(string sesarchQuery)
        {
            if (!string.IsNullOrWhiteSpace(sesarchQuery))
            {
                var list = _pinModelService.SearchPins(sesarchQuery)
                    .Select(x => x.ToPin());
                AllPins = new ObservableCollection<Pin>(list);
            }
            else
            {
                var pins = _pinModelService.GetAllPins().Select(pinModel => pinModel.ToPin());
                AllPins = new ObservableCollection<Pin>(pins);
            }
        }

        private async void OnNavigateToScanQrCode()
        {
            await _dialogService.ShowDialogAsync(nameof(QrCodeScanDialogView));
        }


        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(QrCodeScanDialogViewModel), out Pin pin))
            {
                MoveCameraToPin = MapSpan.FromCenterAndRadius(pin.Position, new Distance(10000));
            }

            if (parameters.ContainsKey("Pins"))
            {
                var list = new ObservableCollection<PinModel>();
                list = parameters.GetValue<ObservableCollection<PinModel>>("Pins");
                AllPins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
            }

            if (parameters.TryGetValue("SelectedItemFromPinTab", out PinModel pinModel))
            {
                Position position = new Position(pinModel.Latitude, pinModel.Longitude);
                MoveCameraToPin = MapSpan.FromCenterAndRadius(position, new Distance(10000));
            }
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(TapOnPin) &&
                TapOnPin != null)
            {
                var selectedPin = new DialogParameters();
                selectedPin.Add(nameof(TapOnPin), TapOnPin);
                await _dialogService.ShowDialogAsync(nameof(Dialogs.TapOnPin), selectedPin);
            }
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var pins = _pinModelService.GetAllPins()
                .Select(pinModel => pinModel.ToPin());

            AllPins = new ObservableCollection<Pin>(pins);

            //PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
        }

        #endregion
    }
}
