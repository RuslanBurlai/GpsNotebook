using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        private IPinModelService _pinLocationRepository;
        private IAuthorizationService _authorization;
        private IPageDialogService _pageDialogService;
        public AddPinViewModel(
            INavigationService navigationService,
            IPinModelService pinLocationRepository,
            IAuthorizationService authorization,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Add pin";

            _pinLocationRepository = pinLocationRepository;
            _authorization = authorization;
            _pageDialogService = pageDialogService;

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

            _pinLocationRepository.AddPin(newPin);
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
