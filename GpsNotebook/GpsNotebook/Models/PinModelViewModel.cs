using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GpsNotebook.Models
{
    public class PinModelViewModel : BindableBase
    {
        #region -- Public Property --

        private int _pinId;
        public int PinId
        {
            get { return _pinId; }
            set { SetProperty(ref _pinId, value); }
        }

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

        private double _pinLatitude;
        public double PinLatitude
        {
            get { return _pinLatitude; }
            set { SetProperty(ref _pinLatitude, value); }
        }

        private double _pinLongitude;
        public double PinLongitude
        {
            get { return _pinLongitude; }
            set { SetProperty(ref _pinLongitude, value); }
        }

        #endregion
    }
}
