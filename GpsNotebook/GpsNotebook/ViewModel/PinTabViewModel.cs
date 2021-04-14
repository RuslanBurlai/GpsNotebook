using GpsNotebook.Extensions;
using GpsNotebook.Models;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class PinTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinModelService pinLocationRepository) :
            base(navigationPage)
        {
            //to resources
            Title = "List pins";

            _pinModelService = pinLocationRepository;
        }

        #region -- Public properties --

        private ObservableCollection<PinModel> _pins;
        public ObservableCollection<PinModel> Pins
        {
            get { return _pins; }
            set { SetProperty(ref _pins, value); }
        }

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private PinModel _selectedItemInListView;
        public PinModel SelectedItemInListView
        {
            get { return _selectedItemInListView; }
            set { SetProperty(ref _selectedItemInListView, value); }
        }

        private ICommand _editPinInListCommand;
        public ICommand EditPinInListCommand =>
            _editPinInListCommand ?? (_editPinInListCommand = new DelegateCommand<PinModel>(OnEditPinInListCommand));

        private ICommand _deletePinFromListCommand;
        public ICommand DeletePinFromListCommand =>
            _deletePinFromListCommand ?? (_deletePinFromListCommand = new DelegateCommand<PinModel>(OnDeletePinFromListCommand));

        private ICommand _searchPinsCommand;
        public ICommand SearchPinsCommand =>
            _searchPinsCommand ?? (_searchPinsCommand = new Command<string>(OnSearchPinsCommand));

        private bool _visibleDropDown;
        public bool VisibleDropDown
        {
            get { return _visibleDropDown; }
            set { SetProperty(ref _visibleDropDown, value); }
        }

        #endregion

        #region -- Private Helpers --

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private void OnEditPinInListCommand(PinModel pin)
        {
        }

        private void OnDeletePinFromListCommand(PinModel pin)
        {
            _pinModelService.DeletePinLocation(pin);
            var myObservableCollection = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
            Pins = myObservableCollection;
        }

        private void OnSearchPinsCommand(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                VisibleDropDown = true;
                var searchPins = _pinModelService.SearchPins(searchQuery);
                Pins = new ObservableCollection<PinModel>(searchPins);
            }
            else
            {
                VisibleDropDown = false;
                var pins = _pinModelService.GetAllPins();
                Pins = new ObservableCollection<PinModel>(pins);
            }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(Pins), this.Pins);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedItemInListView))
            {
                NavigationParameters p = new NavigationParameters();
                p.Add("SelectedItemFromPinTab", SelectedItemInListView);
                NavigationService.NavigateAsync(nameof(MapTabbedView), p);
            }
        }

        #endregion
    }
}
