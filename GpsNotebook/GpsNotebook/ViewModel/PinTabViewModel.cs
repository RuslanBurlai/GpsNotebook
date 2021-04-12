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
            _editPinLocation ?? (_editPinLocation = new DelegateCommand(ExecuteEditPinLocation));

        private void ExecuteEditPinLocation()
        {
            throw new NotImplementedException();
        }

        private ICommand _deletePinLocation;
        public ICommand DeletePinLocation =>
            _deletePinLocation ?? (_deletePinLocation = new DelegateCommand<PinLocation>(ExecuteDeletePinLocation));

        private void ExecuteDeletePinLocation(PinLocation obj)
        {
            _pinLocationRepository.DeletePinLocation(obj);
            var myObservableCollection = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
            Pins = myObservableCollection;
        }
        #endregion

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
            var myObservableCollection = new ObservableCollection<PinLocation>(_pinLocationRepository.GetPinsLocation());
            Pins = myObservableCollection;
        }
    }
}
