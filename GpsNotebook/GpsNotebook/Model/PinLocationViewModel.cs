using GpsNotebook.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GpsNotebook.Model
{
    public class PinLocationViewModel : BindableBase
    {
        public PinLocationViewModel()
        {
            PinLocation = new PinLocation();
        }

        private ICommand _tapOnListView;
        public ICommand TapOnPinView
        {
        }

        public PinLocation PinLocation { get; private set; }

        private string _pinName;
        public string PinName
        {
            get { return _pinName; }
            set { SetProperty(ref _pinName, value); }
        }

        private string _pinDescription;
        public string PinDescription
        {
            get { return _pinDescription; }
            set { SetProperty(ref _pinDescription, value); }
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

    }
}
