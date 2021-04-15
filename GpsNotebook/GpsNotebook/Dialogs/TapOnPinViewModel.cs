using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;
using Newtonsoft.Json;

namespace GpsNotebook.Dialogs
{
    public class TapOnPinViewModel : BindableBase, IDialogAware
    {
        #region -- Public Property --

        private string _pinName;
        public string PinName
        {
            get { return _pinName; }
            set { SetProperty(ref _pinName, value); }
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

        private string _pinDescription;
        public string PinDescription
        {
            get { return _pinDescription; }
            set { SetProperty(ref _pinDescription, value); }
        }

        private string _qrCodeData = "1337";
        public string QrCodeData 
        {
            get { return _qrCodeData; }
            set { SetProperty(ref _qrCodeData, value); }
        }

        private bool _isVisibleQrCode;
        public bool IsVisibleQrCode
        {
            get { return _isVisibleQrCode; }
            set { SetProperty(ref _isVisibleQrCode, value); }
        }

        private ICommand _sharePinViaQrCode;
        public ICommand SharePinViaQrCode =>
            _sharePinViaQrCode ?? (_sharePinViaQrCode = new DelegateCommand(OnsharePinViaQrCodeCommand));

        #endregion

        #region -- Private Helpers --

        private void OnsharePinViaQrCodeCommand()
        {
            if (IsVisibleQrCode == false)
            {
                IsVisibleQrCode = true;
            }
            else
            {
                IsVisibleQrCode = false;
            }
        }

        #endregion

        #region -- IDialogAware implementation --

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //to constants
            if (parameters.ContainsKey("TapOnPin"))
            {
                var pin = new Pin();
                pin = parameters.GetValue<Pin>("TapOnPin");
                PinName = pin.Label;
                PinLatitude = pin.Position.Latitude.ToString();
                PinLongitude = pin.Position.Longitude.ToString();
                string json = JsonConvert.SerializeObject(pin);
                QrCodeData = json;
            }
        }

        #endregion
    }
}
