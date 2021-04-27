﻿using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Validators;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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

        private string _favoritPin;
        public string FavoritPin
        {
            get { return _favoritPin; }
            set { SetProperty(ref _favoritPin, value); }
        }

        private ICommand _savePinCommand;
        public ICommand SavePinCommand =>
            _savePinCommand ?? (_savePinCommand = new DelegateCommand(OnSavePin));

        public ICommand GetPosition => new Command<Position>(ExecuteGetPosition);

        private ICommand _mapTabbedViewCommand;
        public ICommand MapTabbedViewCommand =>
            _mapTabbedViewCommand ?? (_mapTabbedViewCommand = new DelegateCommand(OnMapTabbedView));

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

        #endregion

        #region -- Private Helpers --

        private async void OnMapTabbedView()
        {
            await NavigationService.GoBackAsync();
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
                FavoritPin = "ic_like_grey"
            };

            //List<PinModel> l = _pinLocationRepository.GetAllPins().ToList();

            _pinLocationRepository.AddPin(newPin);
            var parametrs = new NavigationParameters();
            parametrs.Add(nameof(PinModel), newPin);

            await NavigationService.GoBackAsync(parametrs);
        }

        private void ExecuteGetPosition(Position obj)
        {
            PinLatitude = obj.Latitude.ToString();
            PinLongitude = obj.Longitude.ToString();
        }

        #endregion
    }
}
