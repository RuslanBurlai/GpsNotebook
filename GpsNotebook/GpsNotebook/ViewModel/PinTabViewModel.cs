using GpsNotebook.Model;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
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

        #endregion

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = _pinLocationRepository.GetPinsLocation() as ObservableCollection<PinLocation>;
        }
    }
}
