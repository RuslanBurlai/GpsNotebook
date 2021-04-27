using GpsNotebook.View;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using ZXing;

namespace GpsNotebook.ViewModel
{
    public class ScanningQrCodeViewModel : ViewModelBase
    {
        public ScanningQrCodeViewModel(INavigationService naviagtionService) :
            base(naviagtionService)
        {
            Title = "Scan QR code";
        }

        #region -- Public property --

        private bool _isScanning = true;
        public bool IsScanning
        {
            get { return _isScanning; }
            set { SetProperty(ref _isScanning, value); }
        }

        private ICommand _qrScanResaltCommand;
        public ICommand QrScanResaltCommand =>
            _qrScanResaltCommand ?? (_qrScanResaltCommand = new DelegateCommand<Result>(OnQrScanResalt));

        private ICommand _mapTabCommand;
        public ICommand MapTabCommand =>
            _mapTabCommand ?? (_mapTabCommand = new DelegateCommand(OnMapTab));

        private Result _qrResult;
        public Result QrResult
        {
            get { return _qrResult; }
            set { SetProperty(ref _qrResult, value); }
        }

        #endregion

        #region -- Private Helpers --
        private async void OnMapTab()
        {
           await NavigationService.GoBackAsync();
        }

        private async void OnQrScanResalt(Result result)
        {
            //IsScanning = false;

            //var pin = JsonConvert.DeserializeObject<Pin>(result.Text);
            //var qrResalt = new NavigationParameters();

            //qrResalt.Add(nameof(ScanningQrCodeViewModel), pin);

            await NavigationService.GoBackAsync();

            //await NavigationService.NavigateAsync(nameof(MapTabView), qrResalt);
            //await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MapTabView)}");
            //await NavigationService.GoBackAsync();
            //$"/{nameof(NavigationPage)}/{nameof(MapTabbedView)}", qrResalt
        }

        #endregion
    }
}
