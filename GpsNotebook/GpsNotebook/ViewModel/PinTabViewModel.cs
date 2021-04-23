using GpsNotebook.Dialogs;
using GpsNotebook.Extensions;
using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
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
        private IAuthorizationService _authorizationService;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinModelService pinModelService,
            IAuthorizationService authorizationService) :
            base(navigationPage)
        {
            //to resources
            Title = "Pins";

            _pinModelService = pinModelService;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private ObservableCollection<PinViewModel> _pins;
        public ObservableCollection<PinViewModel> Pins
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

        private ICommand _logOutCommand;
        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut));

        private ICommand _settingsViewCommand;
        public ICommand SettingsViewCommand =>
            _settingsViewCommand ?? (_settingsViewCommand = new DelegateCommand(OnSettingsView));

        private ICommand _setFavoritPinCommand;
        public ICommand SetFavoritPinCommand =>
            _setFavoritPinCommand ?? (_setFavoritPinCommand = new DelegateCommand(OnSetFavoritPin));

        private void OnSetFavoritPin()
        {
        }

        private void OnSettingsView()
        {

        }

        private bool _visibleDropDown;
        public bool VisibleDropDown
        {
            get { return _visibleDropDown; }
            set { SetProperty(ref _visibleDropDown, value); }
        }

        private object _sotrValue;
        public object SotrValue
        {
            get { return _sotrValue; }
            set { SetProperty(ref _sotrValue, value); }
        }



        #endregion

        #region -- Private Helpers --

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private async void OnLogOut()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(LogInView)}");
        }

        private void OnEditPinInListCommand(PinModel pin)
        {
        }

        private void OnDeletePinFromListCommand(PinModel pin)
        {
            _pinModelService.DeletePinLocation(pin);
            var myObservableCollection = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
            Pins = myObservableCollection;
        }

        private void OnSearchPinsCommand(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                VisibleDropDown = true;
                var searchPins = _pinModelService.SearchPins(searchQuery);
                Pins = new ObservableCollection<PinViewModel>(searchPins.Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
            }
            else
            {
                VisibleDropDown = false;
                var pins = _pinModelService.GetAllPins();
                Pins = new ObservableCollection<PinViewModel>(pins.Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
            }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if(parameters.TryGetValue(nameof(PinModel),out PinModel newPin))
            {
                Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(Pins), this.Pins);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedItemInListView):
                    {
                        NavigationParameters pinParametrs = new NavigationParameters();
                        pinParametrs.Add("SelectedItemFromPinTab", SelectedItemInListView);
                        await NavigationService.NavigateAsync(nameof(MapTabbedView), pinParametrs);
                        break;
                    }

                case nameof(SotrValue):
                    {
                        var sortMethod = SotrValue as string;
                        var sortedPins = _pinModelService.GetAllPins().Where((pin) => pin.Categories == sortMethod);

                        Pins = new ObservableCollection<PinViewModel>(sortedPins.Select(x => x.ToPinViewModel(SetFavoritPinCommand)));
                        //IsVisibleCategorySelector = false;
                        break;
                    }
            }
        }

        #endregion
    }
}
