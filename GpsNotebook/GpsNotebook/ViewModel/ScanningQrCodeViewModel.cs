using GpsNotebook.View;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;
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

        private ICommand _qrScanResaltCommand;
        public ICommand QrScanResaltCommand =>
            _qrScanResaltCommand ?? (_qrScanResaltCommand = new DelegateCommand<Result>(OnQrScanResalt));

        private ICommand _mapTabCommand;
        public ICommand MapTabCommand =>
            _mapTabCommand ?? (_mapTabCommand = new DelegateCommand(OnMapTab));

        private void OnMapTab()
        {
            NavigationService.NavigateAsync(nameof(MapTabView));
        }

        private async void OnQrScanResalt(Result resalt)
        {
            var pin = JsonConvert.DeserializeObject<Pin>(resalt.Text);
            var qrResalt = new NavigationParameters();

            qrResalt.Add(nameof(ScanningQrCodeViewModel), pin);
            await NavigationService.NavigateAsync(nameof(MapTabbedView), qrResalt);
        }
    }
}
