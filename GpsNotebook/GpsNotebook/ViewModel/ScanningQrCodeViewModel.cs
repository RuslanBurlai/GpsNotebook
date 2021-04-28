using GpsNotebook.View;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
//using Prism.Navigation.Xaml;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using ZXing;

namespace GpsNotebook.ViewModel
{
    public class ScanningQrCodeViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;

        public ScanningQrCodeViewModel(
            INavigationService naviagtionService,
            IPageDialogService pageDialogService) :
            base(naviagtionService)
        {
            Title = "Scan QR code";
            _pageDialogService = pageDialogService;
        }

        #region -- Public property --

        private ICommand _qrScanResultCommand;
        public ICommand QrScanResultCommand =>
            _qrScanResultCommand ?? (_qrScanResultCommand = new Command<Result>(OnQrScanResult));

        private ICommand _mapTabbedCommand;
        public ICommand MapTabbedCommand =>
            _mapTabbedCommand ?? (_mapTabbedCommand = new DelegateCommand(OnMapTab));

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

        private void OnQrScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var q = QrResult;
                var pin = JsonConvert.SerializeObject(result.Text);
                var qrResalt = new NavigationParameters();
                qrResalt.Add(nameof(ScanningQrCodeViewModel), pin);
                //await NavigationService.NavigateAsync(nameof(MapTabbedView), qrResalt);
                await NavigationService.GoBackAsync(qrResalt);
            });
        }

        #endregion
    }
}
