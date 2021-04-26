using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        private ICommand _scanQrCode;
        public ICommand ScanQrCode =>
            _scanQrCode ?? (_scanQrCode = new DelegateCommand(OnScanQrCode));


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