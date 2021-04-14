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
        private IPinModelService _pinLocationRepository;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinModelService pinLocationRepository) :
            base(navigationPage)
        {
            //to resources
            Title = "List pins";

            _pinLocationRepository = pinLocationRepository;
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

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private PinModel _selectedItemInListView;
        public PinModel SelectedItemInListView
        {
            get { return _selectedItemInListView; }
            set { SetProperty(ref _selectedItemInListView, value); }
        }

        private ICommand _editPinLocation;
        public ICommand EditPinLocation =>
            _editPinLocation ?? (_editPinLocation = new DelegateCommand<PinModel>(ExecuteEditPinLocation));

        private void ExecuteEditPinLocation(PinModel obj)
        {
        }

        private ICommand _deletePinLocation;
        public ICommand DeletePinLocation =>
            _deletePinLocation ?? (_deletePinLocation = new DelegateCommand<PinModel>(OnDeletePin));

        private void OnDeletePin(PinModel obj)
        {
            _pinLocationRepository.DeletePinLocation(obj);
            var myObservableCollection = new ObservableCollection<PinModel>(_pinLocationRepository.GetAllPins());
            Pins = myObservableCollection;
        }

        //rename to SearchPinsCommand
        private ICommand _searchPinsCommand;
        public ICommand SearchPinsCommand =>
            _searchPinsCommand ?? (_searchPinsCommand = new Command<string>(OnSearchPinsCommand));

        private void OnSearchPinsCommand(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                //to PinService
                var list = _pinLocationRepository.SearchPins(searchQuery)
                    .Where((x) => x.PinName.ToLower().Contains(searchQuery.ToLower()) ||
                    x.Description.ToLower().Contains(searchQuery.ToLower()));

                //remove myObservableCollection
                var myObservableCollection = new ObservableCollection<PinModel>(list);
                Pins = myObservableCollection;
            }
            else
            {
                var myObservableCollection = new ObservableCollection<PinModel>(_pinLocationRepository.GetAllPins());
                Pins = myObservableCollection;
            }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var myObservableCollection = new ObservableCollection<PinModel>(_pinLocationRepository.GetAllPins());
            Pins = myObservableCollection;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            var myObservableCollection = new ObservableCollection<PinModel>(_pinLocationRepository.GetAllPins());
            Pins = myObservableCollection;
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
