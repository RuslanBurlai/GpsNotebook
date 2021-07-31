using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using ZXing;

namespace GpsNotebook.ViewModel
{
    public class ScanningQrCodeViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;
        private IAuthorizationService _authorization;
        public ScanningQrCodeViewModel(
            INavigationService naviagtionService,
            IPinModelService pinModelService,
            IAuthorizationService authorization) :
            base(naviagtionService)
        {
            Title = "Scan QR code";
            _pinModelService = pinModelService;
            _authorization = authorization;
        }

        #region -- Public property --

        private ICommand _qrScanResultCommand;
        public ICommand QrScanResultCommand =>
            _qrScanResultCommand ?? (_qrScanResultCommand = new Command<Result>(OnQrScanResult));

        private ICommand _mapTabbedCommand;
        public ICommand MapTabbedCommand =>
            _mapTabbedCommand ?? (_mapTabbedCommand = new DelegateCommand(OnMapTab));

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
                var qrPin = JsonConvert.DeserializeObject<Pin>(result.Text);

                _pinModelService.AddPin(CreatePin(qrPin));

                var qrResalt = new NavigationParameters();
                qrResalt.Add(nameof(ScanningQrCodeViewModel), qrPin);
                await NavigationService.GoBackAsync(qrResalt);
            });
        }

        private PinModel CreatePin(Pin pin)
        {
            var newPin = new PinModel
            {
                Latitude = pin.Position.Latitude,
                Longitude = pin.Position.Longitude,
                PinName = pin.Label,
                UserId = _authorization.GetUserId,
            };
            return newPin;
        }

        #endregion
    }
}
