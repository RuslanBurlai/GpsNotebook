using GpsNotebook.Models;
using GpsNotebook.Services.AppThemeService;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Styles;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IAuthorizationService _authorization;
        private IPageDialogService _pageDialogService;
        private IAppThemeService _appThemeService;
        public AddPinViewModel(
            INavigationService navigationService,
            IPinModelService pinModelService,
            IAuthorizationService authorization,
            IPageDialogService pageDialogService,
            IAppThemeService appThemeService) :
            base(navigationService)
        {
            Title = "Add pin";

            _pinModelService = pinModelService;
            _authorization = authorization;
            _pageDialogService = pageDialogService;
            _appThemeService = appThemeService;

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

        #region --  Public properties --

        private string _pinName;
        public string PinName
        {
            get { return _pinName; }
            set { SetProperty(ref _pinName, value); }
        }

        private string _pinDescription;
        public string PinDescription
        {
            get { return _pinDescription; }
            set { SetProperty(ref _pinDescription, value); }
        }

        private string _pinLatitude;
        public string PinLatitude
        {
            get { return _pinLatitude; }
            set { SetProperty(ref _pinLatitude, value); }
        }

        private string _pinLongitude;
        public string PinLongitude
        {
            get { return _pinLongitude; }
            set { SetProperty(ref _pinLongitude, value); }
        }

        private string _clearButtonImagePath;
        public string ClearButtonImagePath
        {
            get { return _clearButtonImagePath; }
            set { SetProperty(ref _clearButtonImagePath, value); }
        }

        private string _clearDescriptionImagePath;
        public string ClearDescriptionImagePath
        {
            get { return _clearDescriptionImagePath; }
            set { SetProperty(ref _clearDescriptionImagePath, value); }
        }

        private List<CategoriesForPin> _categories;
        public List<CategoriesForPin> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private CategoriesForPin _selectedCategories;
        public CategoriesForPin SelectedCategories
        {
            get { return _selectedCategories; }
            set { SetProperty(ref _selectedCategories, value); }
        }

        private ICommand _savePinCommand;
        public ICommand SavePinCommand =>
            _savePinCommand ?? (_savePinCommand = new DelegateCommand(OnSavePin, CanSavePin));

        private bool CanSavePin()
        {
            return Validator.AllFieldsIsNullOrEmpty(PinName, PinDescription, PinLatitude, PinLongitude) &&
                SelectedCategories != null;
        }

        private MapStyle _customMapStyle;
        public MapStyle CustomMapStyle
        {
            get { return _customMapStyle; }
            set { SetProperty(ref _customMapStyle, value); }
        }

        private ICommand _getPositionCommand;
        public ICommand GetPositionCommand => 
            _getPositionCommand ??(_getPositionCommand = new Command<Position>(OnGetPosition));

        private ICommand _mapTabbedViewCommand;
        public ICommand MapTabbedViewCommand =>
            _mapTabbedViewCommand ?? (_mapTabbedViewCommand = new DelegateCommand(OnMapTabbedView));

        private ICommand _clearLabelEntryTextCommand;
        public ICommand ClearLabelEntryTextCommand =>
            _clearLabelEntryTextCommand ?? (_clearLabelEntryTextCommand = new DelegateCommand(OnClearLabelEntryText));

        private ICommand _clearDescriptionEntryCommand;
        public ICommand ClearDescriptionEntryCommand =>
            _clearDescriptionEntryCommand ?? (_clearDescriptionEntryCommand = new DelegateCommand(OnClearDescriptionEntry));

        #endregion

        #region -- Overrides --
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (_appThemeService.IsDarkTheme)
            {
                CustomMapStyle = _appThemeService.SetMapTheme(nameof(DarkTheme));
            }
            else
            {
                CustomMapStyle = _appThemeService.SetMapTheme(nameof(LightTheme));
            }

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<PinViewModel>("PinForEdit", out PinViewModel EditingPin))
            {
                PinName = EditingPin.PinName;
                PinLatitude = EditingPin.PinLatitude.ToString();
                PinLongitude = EditingPin.PinLongitude.ToString();
                PinDescription = EditingPin.PinDescription;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(PinName):
                    {
                        if (!string.IsNullOrWhiteSpace(PinName))
                        {
                            ClearButtonImagePath = "ic_clear";
                        }
                        else
                        {
                            ClearButtonImagePath = string.Empty;
                        }
                        break; 
                    }

                case nameof(PinDescription):
                    {
                        if (!string.IsNullOrWhiteSpace(PinDescription))
                        {
                            ClearDescriptionImagePath = "ic_clear";
                        }
                        else
                        {
                            ClearDescriptionImagePath = string.Empty;
                        }
                        break;
                    }
            }
        }

        #endregion

        #region -- Private Helpers --

        private void OnClearLabelEntryText()
        {
            PinName = string.Empty;
        }

        private async void OnMapTabbedView()
        {
            await NavigationService.NavigateAsync(nameof(MapTabbedView));
        }

        private async void OnSavePin()
        {
            var newPin = new PinModel
            {
                Description = PinDescription,
                Latitude = double.Parse(PinLatitude),
                Longitude = double.Parse(PinLongitude),
                PinName = PinName,
                Categories = SelectedCategories.Name,
                UserId = _authorization.GetUserId,
                FavoritPin = "ic_like_gray"
            };

            _pinModelService.AddPin(newPin);
            var parametrs = new NavigationParameters();
            parametrs.Add(nameof(PinModel), newPin);

            await NavigationService.GoBackAsync(parametrs);
        }

        private void OnGetPosition(Position position)
        {
            PinLatitude = position.Latitude.ToString();
            PinLongitude = position.Longitude.ToString();
        }

        private void OnClearDescriptionEntry()
        {
            PinDescription = string.Empty;
        }

        #endregion
    }
}
