using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GpsNotebook.Models
{
    public class PinViewModel : BindableBase, IEntityBaseForModel
    {
        #region -- Public Property --

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
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

        private string _pinCategories;
        public string PinCategories
        {
            get { return _pinCategories; }
            set { SetProperty(ref _pinCategories, value); }
        }

        private string _imageFavoritPin;
        public string ImageFavoritPin
        {
            get { return _imageFavoritPin; }
            set { SetProperty(ref _imageFavoritPin, value); }
        }

        #endregion
    }
}
