using GpsNotebook.Services.AppThemeService;
using GpsNotebook.Styles;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private IAppThemeService _appThemeService;
        public SettingsViewModel(
            INavigationService navigationService,
            IAppThemeService appThemeService) :
            base(navigationService)
        {
            Title = "Settings";
            _appThemeService = appThemeService;
        }

        #region -- Public Property --

        private ICommand _mapTabbedCommand;
        public ICommand MapTabbedCommand =>
            _mapTabbedCommand ?? (_mapTabbedCommand = new DelegateCommand(OnMapTabbed));

        private ICommand _scanQrCodeCommand;
        public ICommand ScanQrCodeCommand =>
            _scanQrCodeCommand ?? (_scanQrCodeCommand = new DelegateCommand(OnScanQrCode));

        private bool _isDarkTheme;
        public bool IsDarkTheme 
        {
            get { return _isDarkTheme; }
            set { SetProperty(ref _isDarkTheme, value); }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsDarkTheme))
            {
                if (IsDarkTheme)
                {
                    _appThemeService.SetUIAppTheme(nameof(DarkTheme));
                    _appThemeService.SetMapTheme(nameof(DarkTheme));

                }
                else
                {
                    _appThemeService.SetMapTheme(nameof(LightTheme));
                    _appThemeService.SetUIAppTheme(nameof(LightTheme));
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue<string>(nameof(ScanningQrCodeViewModel), out string pin))
            {
                var qrResult = new NavigationParameters();
                qrResult.Add(nameof(SettingsView), pin);
                NavigationService.NavigateAsync(nameof(MapTabbedView), qrResult);
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            IsDarkTheme = _appThemeService.IsDarkTheme;
        }

        #endregion

        #region -- Private Helpers --

        private async void OnMapTabbed()
        {
            await NavigationService.NavigateAsync(nameof(MapTabbedView));
        }

        private async void OnScanQrCode()
        {
            await NavigationService.NavigateAsync(nameof(ScanningQrCodeView));
        }

        #endregion
    }
}