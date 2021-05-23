using GpsNotebook.Services.PinLocationRepository;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using GpsNotebook.Extensions;
using GpsNotebook.Models;
using System.ComponentModel;
using Prism.Services.Dialogs;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Prism.Commands;
using GpsNotebook.Services.Authorization;
using GpsNotebook.View;
using Newtonsoft.Json;
using GpsNotebook.Dialogs;
using System.Collections.Generic;
using GpsNotebook.Services.AppThemeService;
using GpsNotebook.Styles;

namespace GpsNotebook.ViewModel
{
    public class MapTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IDialogService _dialogService;
        private IAuthorizationService _authorizationService;
        private IAppThemeService _appThemeService;
        //private IPermissionService _permissionService;

        public MapTabViewModel(
            INavigationService navigationService,
            IPinModelService pinModelService,
            IDialogService dialogService,
            IAuthorizationService authorizationService,
            IAppThemeService appThemeService) :
            base(navigationService)
        {
            Title = "Map";

            _pinModelService = pinModelService;
            _dialogService = dialogService;
            _authorizationService = authorizationService;
            _appThemeService = appThemeService;
            //IPermissionService permissionService_permissionService = permissionService;

            Pins = new ObservableCollection<Pin>();

            Categories = new List<CategoriesForPin>
            {
                new CategoriesForPin { Name = "Gyms" },
                new CategoriesForPin { Name = "Restaurants" },
                new CategoriesForPin { Name = "Hotels" },
                new CategoriesForPin { Name = "Supermarkets" },
                new CategoriesForPin { Name = "Schools" },
                new CategoriesForPin { Name = "Place to rest" },
                new CategoriesForPin { Name = "Work" },
                new CategoriesForPin { Name = "Home" },
                new CategoriesForPin { Name = "Airports" },
                new CategoriesForPin { Name = "Football stadium" }
            };

        }

        #region -- Public properties --

        private ObservableCollection<Pin> _allPins;
        public ObservableCollection<Pin> Pins
        {
            get { return _allPins; }
            set { SetProperty(ref _allPins, value); }
        }

        private List<CategoriesForPin> _categories;
        public List<CategoriesForPin> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Pin _tapOnPin;
        public Pin TapOnPin
        {
            get { return _tapOnPin; }
            set { SetProperty(ref _tapOnPin, value); }
        }

        private bool _showInfoAboutPin;
        public bool ShowInfoAboutPin
        {
            get { return _showInfoAboutPin; }
            set { SetProperty(ref _showInfoAboutPin, value); }
        }

        private MapSpan _moveCameraToPin;
        public MapSpan MoveCameraToPin
        {
            get { return _moveCameraToPin; }
            set { SetProperty(ref _moveCameraToPin, value); }
        }

        private string _searchingText;
        public string SearchingText
        {
            get { return _searchingText; }
            set { SetProperty(ref _searchingText, value); }
        }

        private string _showClearImageButtom;
        public string ShowClearImageButtom
        {
            get { return _showClearImageButtom; }
            set { SetProperty(ref _showClearImageButtom, value); }
        }

        private bool _searchBarSpaned;
        public bool SearchBarSpaned
        {
            get { return _searchBarSpaned; }
            set { SetProperty(ref _searchBarSpaned, value); }
        }

        private string _qrCodeData;
        public string QrCodeData
        {
            get { return _qrCodeData; }
            set { SetProperty(ref _qrCodeData, value); }
        }

        private bool _showDropDown;
        public bool ShowDropDown
        {
            get { return _showDropDown; }
            set { SetProperty(ref _showDropDown, value); }
        }

        private MapStyle _customMapStyle;
        public MapStyle CustomMapStyle
        {
            get { return _customMapStyle; }
            set { SetProperty(ref _customMapStyle, value); }
        }

        private bool _isSelecletCategory;
        public bool IsSelecletCategory
        {
            get { return _isSelecletCategory; }
            set { SetProperty(ref _isSelecletCategory, value); }
        }

        private ICommand _logOutCommand;
        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut));

        private ICommand _mapClickCommand;
        public ICommand MapClickCommand =>
            _mapClickCommand ?? (_mapClickCommand = new Command(OnMapClick));

        private ICommand _settingsCommand;
        public ICommand SettingsCommand =>
            _settingsCommand ?? (_settingsCommand = new DelegateCommand(OnSettings));

        private ICommand _selectSortCategoryCommand;
        public ICommand SelectSortCategoryCommand =>
            _selectSortCategoryCommand ?? (_selectSortCategoryCommand = new DelegateCommand<CategoriesForPin>(SelectSortCategory));

        private ICommand _sharePinCommand;
        public ICommand SharePinCommand =>
            _sharePinCommand ?? (_sharePinCommand = new DelegateCommand(OnSharePin));

        private ICommand _tapOnDropDownCellCommand;
        public ICommand TapOnDropDownCellCommand =>
            _tapOnDropDownCellCommand ?? (_tapOnDropDownCellCommand = new DelegateCommand<Pin>(OnTapOnDropDownCell));

        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            //_permissionService.CheckPermission<LocationPermission>();

            if (parameters.TryGetValue("SelectedItemInListView", out PinViewModel pinViewModel))
            {
                var pin = pinViewModel.ToPin();
                MoveCameraToPin = MapSpan.FromCenterAndRadius(pin.Position, new Distance(10000));
            }

            if (parameters.TryGetValue(nameof(Pins), out ObservableCollection<PinViewModel> pins))
            {
                Pins = new ObservableCollection<Pin>(pins
                    .Where(p => p.FavoritPin == "ic_like_blue")
                    .Select(x => x.ToPin()));
            }

            if (parameters.TryGetValue(nameof(SettingsView), out Pin qrResult))
            {
                MoveCameraToPin = MapSpan.FromCenterAndRadius(qrResult.Position, new Distance(10000));
            }
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
                            SearchBarSpaned = true;
                            ShowDropDown = true;

                            ShowClearImageButtom = "ic_clear";

                            var list = _pinModelService.SearchPins(SearchingText)
                                .Where(p => p.FavoritPin == "ic_like_blue")
                                .Select(x => x.ToPinViewModel());

                            Pins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
                        }
                        else
                        {
                            SearchBarSpaned = false;
                            ShowDropDown = false;

                            ShowClearImageButtom = string.Empty;

                            var pins = _pinModelService.GetAllPins()
                                .Where(p => p.FavoritPin == "ic_like_blue")
                                .Select(pinModel => pinModel.ToPinViewModel().ToPin());
                            Pins = new ObservableCollection<Pin>(pins);
                        }

                        break;
                    }

                case nameof(TapOnPin):
                    {
                        try
                        {
                            if (TapOnPin != null)
                            {
                                string json = JsonConvert.SerializeObject(TapOnPin, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
                                QrCodeData = json;
                                ShowInfoAboutPin = true;
                            }

                        }
                        catch (System.Exception e)
                        {
                            ShowInfoAboutPin = true;
                            System.Console.WriteLine(e.Message);
                        }

                        break;
                    }
            }

        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var pins = _pinModelService.GetAllPins()
                .Where(p => p.FavoritPin == "ic_like_blue")
                .Select(pinModel => pinModel.ToPinViewModel().ToPin());

            Pins = new ObservableCollection<Pin>(pins);

            if (_appThemeService.IsDarkTheme)
            {
                CustomMapStyle = _appThemeService.SetMapTheme(nameof(DarkTheme));
            }
            else
            {
                CustomMapStyle = _appThemeService.SetMapTheme(nameof(LightTheme));
            }

        }

        #endregion

        #region -- Private Helpers --

        private void OnTapOnDropDownCell(Pin pin)
        {
            SearchingText = string.Empty;
            ShowDropDown = false;
            MoveCameraToPin = MapSpan.FromCenterAndRadius(pin.Position, new Distance(10000));
        }

        private void OnSharePin()
        {
            var dialogParametr = new DialogParameters();
            dialogParametr.Add(nameof(SharePinCommand), QrCodeData);
            _dialogService.ShowDialog(nameof(SharePinViaQRDialogView), dialogParametr);
        }

        private async void OnLogOut()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInOrRegisterView)}");
        }

        private void OnMapClick()
        {
            IsSelecletCategory = false;
            ShowInfoAboutPin = false;
            var pins = _pinModelService.GetAllPins()
                .Where(p => p.FavoritPin == "ic_like_blue")
                .Select(pinModel => pinModel.ToPinViewModel());
            Pins = new ObservableCollection<Pin>(pins.Select(x => x.ToPin()));
        }

        private void SelectSortCategory(CategoriesForPin category)
        {
            IsSelecletCategory = true;
            var list = _pinModelService.SearchByCategory(category.Name)
                .Where(p => p.FavoritPin == "ic_like_blue")
                .Select((x) => x.ToPinViewModel());
            Pins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
        }

        private async void OnSettings()
        {
            await NavigationService.NavigateAsync(nameof(SettingsView));
        }

        #endregion
    }
}
