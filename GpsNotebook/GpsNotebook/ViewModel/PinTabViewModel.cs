using GpsNotebook.Extensions;
using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
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
        private IPageDialogService _pageDialogService;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinModelService pinModelService,
            IAuthorizationService authorizationService,
            IPageDialogService pageDialogService) :
            base(navigationPage)
        {
            Title = "Pins";

            _pinModelService = pinModelService;
            _authorizationService = authorizationService;
            _pageDialogService = pageDialogService;
        }

        #region -- Public properties --

        private ObservableCollection<PinViewModel> _pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get { return _pins; }
            set { SetProperty(ref _pins, value); }
        }

        private string _likeimage;
        public string LikeImage
        {
            get { return _likeimage; }
            set { SetProperty(ref _likeimage, value); }
        }

        private PinViewModel _selectedItemInListView;
        public PinViewModel SelectedItemInListView
        {
            get { return _selectedItemInListView; }
            set { SetProperty(ref _selectedItemInListView, value); }
        }

        private string _searchingText;
        public string SearchingText
        {
            get { return _searchingText; }
            set { SetProperty(ref _searchingText, value); }
        }

        private bool _isSearchEntrySpaned;
        public bool IsSearchEntrySpaned
        {
            get { return _isSearchEntrySpaned; }
            set { SetProperty(ref _isSearchEntrySpaned, value); }
        }

        private string _showClearImageButtom;
        public string ShowClearImageButtom
        {
            get { return _showClearImageButtom; }
            set { SetProperty(ref _showClearImageButtom, value); }
        }

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private ICommand _editPinInListCommand;
        public ICommand EditPinInListCommand =>
            _editPinInListCommand ?? (_editPinInListCommand = new DelegateCommand<PinViewModel>(OnEditPinInList));

        private ICommand _deletePinFromListCommand;
        public ICommand DeletePinFromListCommand =>
            _deletePinFromListCommand ?? (_deletePinFromListCommand = new DelegateCommand<PinViewModel>(OnDeletePinFromList));

        private ICommand _logOutCommand;
        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut));

        private ICommand _settingsViewCommand;
        public ICommand SettingsViewCommand =>
            _settingsViewCommand ?? (_settingsViewCommand = new DelegateCommand(OnSettingsView));

        private ICommand _setFavoritPinCommand;
        public ICommand SetFavoritPinCommand =>
            _setFavoritPinCommand ?? (_setFavoritPinCommand = new DelegateCommand<object>(OnSetFavoritPin));

        private ICommand _tapOnCellCommand;
        public ICommand TapOnCellCommand =>
            _tapOnCellCommand ?? (_tapOnCellCommand = new DelegateCommand<PinViewModel>(OnTapOnCell));

        #endregion

        #region -- Private Helpers --

        private void OnSetFavoritPin(object item)
        {
            var pin = item as PinViewModel;
            pin.FavoritPin = pin.FavoritPin.Equals("ic_like_blue") ? "ic_like_gray" : "ic_like_blue";
            _pinModelService.AddPin(pin.ToPinModel());
        }

        private async void OnSettingsView()
        {
            await NavigationService.NavigateAsync(nameof(SettingsView));
        }

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private async void OnLogOut()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{nameof(LogInOrRegisterView)}");
        }

        private async void OnEditPinInList(PinViewModel pin)
        {
            NavigationParameters pinForEdit = new NavigationParameters();
            pinForEdit.Add("PinForEdit", pin);
            await NavigationService.NavigateAsync(nameof(AddPinView), pinForEdit);
        }

        private async void OnDeletePinFromList(PinViewModel pin)
        {
            var userAnsver = await _pageDialogService.DisplayAlertAsync("Delete","Do you want delete pin?", "Yes", "No");
            if (userAnsver)
            {
                _pinModelService.DeletePin(pin.ToPinModel());
                var newList = _pinModelService.GetAllPins();
                Pins = new ObservableCollection<PinViewModel>(newList.Select(x => x.ToPinViewModel()));
            }
        }

        private async void OnTapOnCell(PinViewModel pinViewModel)
        {
            NavigationParameters pinParametrs = new NavigationParameters();
            pinParametrs.Add("SelectedItemInListView", pinViewModel);
            await NavigationService.NavigateAsync(nameof(MapTabbedView), pinParametrs);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if (parameters.TryGetValue(nameof(PinModel), out PinModel newPin))
            {
                Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel()));
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(Pins), this.Pins);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel()));
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SearchingText):
                    {
                        if (!string.IsNullOrWhiteSpace(SearchingText))
                        {
                            var sortedPins = _pinModelService.SearchPins(SearchingText);
                            ShowClearImageButtom = "ic_clear";

                            Pins = new ObservableCollection<PinViewModel>(sortedPins.Select(x => x.ToPinViewModel()));
                            IsSearchEntrySpaned = true;
                        }
                        else
                        {
                            Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel()));

                            IsSearchEntrySpaned = false;
                            ShowClearImageButtom = string.Empty;
                        }

                        break;
                    }
            }
        }

        #endregion
    }
}
