using GpsNotebook.Styles;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Settings";
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
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();
                    if (IsDarkTheme == true)
                    {
                        mergedDictionaries.Add(new DarkTheme());
                    }
                }
            }
        }

        #endregion

        #region -- Private Helpers --

        private async void OnMapTabbed()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnScanQrCode()
        {
            await NavigationService.NavigateAsync(nameof(ScanningQrCodeView));
        }

        #endregion
    }
}