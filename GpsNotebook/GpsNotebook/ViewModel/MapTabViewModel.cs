using GpsNotebook.Services.PinLocationRepository;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using GpsNotebook.Extensions;
using GpsNotebook.Model;
using System.ComponentModel;
using Prism.Services.Dialogs;
using GpsNotebook.Dialogs;

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

            var p = new PinLocation();
            AllPins = p.PinToMapTabView(_pinLocationRepository.GetPinsLocation());
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

        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            var p = new PinLocation();
            AllPins = p.PinToMapTabView(_pinLocationRepository.GetPinsLocation());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if(args.PropertyName == nameof(SelectedPin))
            {
                _dialogService.ShowDialog(nameof(TapOnPin));
            }
        }
        #endregion
    }
}
