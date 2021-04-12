using GpsNotebook.Model;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class PinTabViewModel : ViewModelBase
    {
        private IPinLocationRepository _pinLocationRepository;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinLocationRepository pinLocationRepository) :
            base(navigationPage)
        {
            //to resources
            Title = "List pins";

            _pinLocationRepository = pinLocationRepository;
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

        private PinLocation _selectedItem;
        public PinLocation SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private ICommand _editPinLocation;
        public ICommand EditPinLocation =>
            _editPinLocation ?? (_editPinLocation = new DelegateCommand<PinLocation>(ExecuteEditPinLocation));

        private void ExecuteEditPinLocation(PinLocation obj)
        {
        }

        private ICommand _deletePinLocation;
        public ICommand DeletePinLocation =>
            _deletePinLocation ?? (_deletePinLocation = new DelegateCommand<PinLocation>(OnDeletePin));

        private void OnDeletePin(PinLocation obj)
        {
            _pinLocationRepository.DeletePinLocation(obj);
            var myObservableCollection = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
            Pins = myObservableCollection;
        }

        //rename to SearchPinsCommand
        private ICommand _searchText;
        public ICommand SearchText =>
            _searchText ?? (_searchText = new Command(ExecuteSearchText));
        
        private void ExecuteSearchText(object obj)
        {
            var searchQuery = obj as string;

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                //to PinService
                var list = Pins.Where((x) => 
                x.PinName.ToLower().Contains(searchQuery.ToLower()) || 
                x.Description.ToLower().Contains(searchQuery.ToLower()));

                //remove myObservableCollection
                var myObservableCollection = new ObservableCollection<PinLocation>(list);
                Pins = myObservableCollection;
            }
            else
            {
                var myObservableCollection = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
                Pins = myObservableCollection;
            }
        }

        public ICommand PinTappedCommand => new Command<PinLocationListViewModel>(OnPinTapped);
        private void OnPinTapped(PinLocationListViewModel obj)
        {
            
        }
        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var myObservableCollection = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
            Pins = myObservableCollection;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            parameters.Add(nameof(this.SelectedItem), this.SelectedItem);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if(args.PropertyName == nameof(SelectedItem))
            {
                NavigationParameters p = new NavigationParameters();
                p.Add("SelectedItemFromPinTab", SelectedItem);
                NavigationService.NavigateAsync(nameof(MapTabbedView), p);
            }
        }

        #endregion
    }
}
