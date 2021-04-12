using GpsNotebook.Services.PinLocationRepository;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using GpsNotebook.Extensions;
using GpsNotebook.Model;
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
        private IPinLocationRepository _pinLocationRepository;
        private IDialogService _dialogService;

        public MapTabViewModel(
            INavigationService navigationService,
            IPinLocationRepository pinLocationRepository,
            IDialogService dialogService) :
            base(navigationService)
        {
            Title = "Map with pins";

            _pinLocationRepository = pinLocationRepository;
            _dialogService = dialogService;

            var pins = _pinLocationRepository.GetPinsLocation().Select(x => x.ToPinModel());
            AllPins = new ObservableCollection<Pin>(pins);
        }

        #region -- Public properties --

        private ObservableCollection<Pin> _allPins;
        public ObservableCollection<Pin> AllPins
        {
            get { return _allPins; }
            set { SetProperty(ref _allPins, value); }
        }

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get { return _selectedPin; }
            set { SetProperty(ref _selectedPin, value); }
        }

        private ICommand _getSearchText;
        public ICommand SearchText =>
            _getSearchText ?? (_getSearchText = new Command<string>(ExecuteGetSearchText));

        private void ExecuteGetSearchText(string sesarchQuery)
        {
            if (!string.IsNullOrWhiteSpace(sesarchQuery))
            {
                //to PinService
                var list = AllPins.Where((x) => 
                x.Label.ToLower().Contains(sesarchQuery.ToLower()));

                var myObservableCollection = new ObservableCollection<Pin>(list);
                AllPins = myObservableCollection;
            }
            else
            {
                //remake
                var p = new PinLocation();
                AllPins = p.PinToMapTabView(_pinLocationRepository.GetPinsLocation());
            }

        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            //remake
            AllPins = new PinLocation().PinToMapTabView(_pinLocationRepository.GetPinsLocation());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //var pin = new PinLocation();
            //if (parameters.TryGetValue<PinLocation>("SelectedItemFromPinTab", out pin)) ;
            //{
            //    AllPins.Add(new Pin
            //    {
            //        Label = pin.PinName,
            //        Position = new Position(pin.Latitude, pin.Longitude)
            //    });
            //}
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedPin) &&
                SelectedPin != null)
            {
                var selectedPin = new DialogParameters();
                selectedPin.Add(nameof(SelectedPin), SelectedPin);
                await _dialogService.ShowDialogAsync(nameof(TapOnPin), selectedPin);
            }
        }
        #endregion
    }
}
