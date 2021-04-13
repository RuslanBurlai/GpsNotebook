using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Dialogs
{
    public class TapOnPinViewModel : BindableBase, IDialogAware
    {
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
            }
        }

        #endregion
    }
}
