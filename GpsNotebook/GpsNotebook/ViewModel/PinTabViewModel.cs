﻿using GpsNotebook.Dialogs;
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

        private string _like;
        public string Like
        {
            get { return _like; }
            set { SetProperty(ref _like, value); }
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

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private ICommand _editPinInListCommand;
        public ICommand EditPinInListCommand =>
            _editPinInListCommand ?? (_editPinInListCommand = new DelegateCommand<PinViewModel>(OnEditPinInList));

        private ICommand _deletePinFromListCommand;
        public ICommand DeletePinFromListCommand =>
            _deletePinFromListCommand ?? (_deletePinFromListCommand = new DelegateCommand<PinViewModel>(OnDeletePinFromListCommand));

        private ICommand _searchPinsCommand;
        public ICommand SearchPinsCommand =>
            _searchPinsCommand ?? (_searchPinsCommand = new Command<string>(OnSearchPins));

        private ICommand _logOutCommand;
        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut));

        private ICommand _settingsViewCommand;
        public ICommand SettingsViewCommand =>
            _settingsViewCommand ?? (_settingsViewCommand = new DelegateCommand(OnSettingsView));

        private ICommand _setFavoritPinCommand;
        public ICommand SetFavoritPinCommand =>
            _setFavoritPinCommand ?? (_setFavoritPinCommand = new DelegateCommand(OnSetFavoritPin));

        #endregion

        #region -- Private Helpers --

        private void OnSetFavoritPin()
        {
            if (Like == "ic_like_gray")
            {
                Like = "ic_like_blue";
            }
            else
            {
                Like = "ic_like_gray";
            }
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
            await NavigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(LogInView)}");
        }

        private void OnEditPinInList(PinViewModel pin)
        {
        }

        private void OnDeletePinFromListCommand(PinViewModel pin)
        {
            _pinModelService.DeletePinLocation(pin.TopPinModel());
            var myObservableCollection = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel()));
            Pins = myObservableCollection;
        }

        private void OnSearchPins(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var searchPins = _pinModelService.SearchPins(searchQuery);
                Pins = new ObservableCollection<PinViewModel>(searchPins.Select(x => x.ToPinViewModel()));
            }
            else
            {
                var pins = _pinModelService.GetAllPins();
                Pins = new ObservableCollection<PinViewModel>(pins.Select(x => x.ToPinViewModel()));
            }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if(parameters.TryGetValue(nameof(PinModel),out PinModel newPin))
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

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedItemInListView):
                    {
                        NavigationParameters pinParametrs = new NavigationParameters();
                        pinParametrs.Add(nameof(SelectedItemInListView), SelectedItemInListView);
                        await NavigationService.NavigateAsync(nameof(MapTabbedView), pinParametrs);
                        break;
                    }

                case nameof(SearchingText):
                    {
                        if (!string.IsNullOrWhiteSpace(SearchingText))
                        {
                            var sortedPins = _pinModelService.SearchPins(SearchingText);

                            Pins = new ObservableCollection<PinViewModel>(sortedPins.Select(x => x.ToPinViewModel()));

                        }
                        else
                        {
                            Pins = new ObservableCollection<PinViewModel>(_pinModelService.GetAllPins().Select(x => x.ToPinViewModel()));
                        }
                        break;
                    }
            }
        }

        #endregion
    }
}
