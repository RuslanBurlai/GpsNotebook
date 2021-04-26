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
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Collections.Generic;
using System;

namespace GpsNotebook.ViewModel
{
    public class MapTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IDialogService _dialogService;
        private IAuthorizationService _authorizationService;

        public MapTabViewModel(
            INavigationService navigationService,
            IPinModelService pinModelService,
            IDialogService dialogService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "Map";

            _pinModelService = pinModelService;
            _dialogService = dialogService;
            _authorizationService = authorizationService;

            AllPins = new ObservableCollection<Pin>();

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
        public ObservableCollection<Pin> AllPins
        {
            get { return _allPins; }
            set { SetProperty(ref _allPins, value); }
        }

        private string _selectedCategories;
        public string SelectedCategories
        {
            get { return _selectedCategories; }
            set { SetProperty(ref _selectedCategories, value); }
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

        private ICommand _logOutCommand;
        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut));

        private ICommand _mapClickCommand;
        public ICommand MapClickCommand =>
            _mapClickCommand ?? (_mapClickCommand = new Command<string>(OnMapClick));

        private ICommand _settingsCommand;
        public ICommand SettingsCommand =>
            _settingsCommand ?? (_settingsCommand = new DelegateCommand(OnSettings));

        private ICommand _selectSortCategoryCommand;
        public ICommand SelectSortCategoryCommand =>
            _selectSortCategoryCommand ?? (_selectSortCategoryCommand = new DelegateCommand<CategoriesForPin>(SelectSortCategory));

        private ICommand _clearSearchBarCommand;
        public ICommand ClearSearchBarCommand =>
            _clearSearchBarCommand ?? (_clearSearchBarCommand = new DelegateCommand(OnClearSearchBar));

        private ICommand _sharePinCommand;
        public ICommand SharePinCommand =>
            _sharePinCommand ?? (_sharePinCommand = new DelegateCommand(OnSharePin));

        #endregion

        #region -- Private Helpers --

        private void OnSharePin()
        {
            var dialogParametr = new DialogParameters();
            dialogParametr.Add(nameof(SharePinCommand), QrCodeData);
             _dialogService.ShowDialog(nameof(SharePinViaQRDialogView), dialogParametr);
        }

        private void OnClearSearchBar()
        {
            SearchingText = string.Empty;
            ShowClearImageButtom = string.Empty;

            var pins = _pinModelService.GetAllPins()
                .Select(pinModel => pinModel.ToPinViewModel().ToPin());

            AllPins = new ObservableCollection<Pin>(pins);

        }

        private async void OnLogOut()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(LogInView)}");
        }

        private void OnMapClick(string sesarchQuery)
        {
            ShowInfoAboutPin = false;
        }

        private void SelectSortCategory(CategoriesForPin category)
        {
            if (!string.IsNullOrWhiteSpace(category.Name))
            {
                var list = _pinModelService.SearchByCategory(category.Name).Select((x) => x.ToPinViewModel());
                AllPins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
            }
            else
            {
                var pins = _pinModelService.GetAllPins().Select(pinModel => pinModel.ToPinViewModel());
                AllPins = new ObservableCollection<Pin>(pins.Select(x => x.ToPin()));
            }
        }

        private async void OnSettings()
        {
            await NavigationService.NavigateAsync(nameof(SettingsView));
            //_dialogService.ShowDialog(nameof(QrCodeScanDialogView), OnDialogClosed);
        }

        private void OnDialogClosed(IDialogResult result)
        {
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Pins"))
            {
                var list = new ObservableCollection<PinViewModel>();
                list = parameters.GetValue<ObservableCollection<PinViewModel>>("Pins");
                AllPins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
            }

            if (parameters.TryGetValue("SelectedItemInListView", out PinViewModel pinViewModel))
            {
                MoveCameraToPin = MapSpan.FromCenterAndRadius(pinViewModel.ToPin().Position, new Distance(10000));
            }

            if (parameters.ContainsKey(nameof(ScanningQrCodeViewModel)))
            {
                var pin = new Pin();
                pin = parameters.GetValue<Pin>(nameof(ScanningQrCodeViewModel));
                AllPins = new ObservableCollection<Pin>();
                AllPins.Add(pin);
                MoveCameraToPin = MapSpan.FromCenterAndRadius(pin.Position, new Distance(10000));
            }

        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SearchingText):
                    {
                        if (SearchingText == string.Empty)
                        {
                            SearchBarSpaned = false;

                            ShowClearImageButtom = string.Empty;
                            var pins = _pinModelService.GetAllPins()
                                .Select(pinModel => pinModel.ToPinViewModel().ToPin());

                            AllPins = new ObservableCollection<Pin>(pins);

                        }
                        else
                        {
                            SearchBarSpaned = true;

                            ShowClearImageButtom = "ic_clear";
                            var list = _pinModelService.SearchPins(SearchingText)
                                .Select(x => x.ToPinViewModel());

                            AllPins = new ObservableCollection<Pin>(list.Select(x => x.ToPin()));
                        }

                        break;
                    }

                case nameof(TapOnPin):
                    {
                        if (TapOnPin != null)
                        {
                            ShowInfoAboutPin = true;
                            //var pin = new Pin();
                            string json = JsonConvert.SerializeObject(TapOnPin);
                            QrCodeData = json;
                        }

                        break;
                    }
            }
            
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var pins = _pinModelService.GetAllPins()
                .Select(pinModel => pinModel.ToPinViewModel().ToPin());

            AllPins = new ObservableCollection<Pin>(pins);
        }

        #endregion
    }
}
