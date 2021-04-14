using GpsNotebook.Services.PinLocationRepository;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using GpsNotebook.Extensions;
using GpsNotebook.Models;
using System.ComponentModel;
using Prism.Services.Dialogs;
using GpsNotebook.Dialogs;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Linq;

namespace GpsNotebook.ViewModel
{
    public class MapTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IDialogService _dialogService;

        public MapTabViewModel(
            INavigationService navigationService,
            IPinModelService pinModelService,
            IDialogService dialogService) :
            base(navigationService)
        {
            Title = "Map with pins";

            _pinModelService = pinModelService;
            _dialogService = dialogService;
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

        private ICommand searchPins;
        public ICommand SearchPins =>
            searchPins ?? (searchPins = new Command<string>(OnGetSearchPins));

        private void OnGetSearchPins(string sesarchQuery)
        {
            if (!string.IsNullOrWhiteSpace(sesarchQuery))
            {
                var list = _pinModelService.SearchPins(sesarchQuery)
                    .Select(x => x.ToPin());

                var searchResponce = new ObservableCollection<Pin>(list);
                AllPins = searchResponce;
            }
            else
            {
                var pins = _pinModelService.GetAllPins().Select(x => x.ToPin());
                AllPins = new ObservableCollection<Pin>(pins);
            }
        }

        private MapSpan _moveCameraToPin;
        public MapSpan MoveCameraToPin
        {
            get { return _moveCameraToPin; }
            set { SetProperty(ref _moveCameraToPin, value); }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            var pins = _pinModelService.GetAllPins()
                .Select(pinModel => pinModel.ToPin());

            AllPins = new ObservableCollection<Pin>(pins);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var p = new PinModel();
            if (parameters.TryGetValue<PinModel>("SelectedItemFromPinTab", out p))
            {
                Position position = new Position(p.Latitude, p.Longitude);
                MoveCameraToPin = MapSpan.FromCenterAndRadius(position, new Distance(100000));
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

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var pins = _pinModelService.GetAllPins()
                .Select(pinModel => pinModel.ToPin());

            AllPins = new ObservableCollection<Pin>(pins);
        }

        #endregion
    }
}
