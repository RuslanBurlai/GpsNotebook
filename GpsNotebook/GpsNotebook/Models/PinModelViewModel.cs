using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GpsNotebook.Models
{
    public class PinModelViewModel : BindableBase
    {
        private ICommand _tapOnListView;
        public ICommand TapOnListView =>
            _tapOnListView ?? (_tapOnListView = new DelegateCommand<PinModelViewModel>(OnTapOnListViewCommand));

        private void OnTapOnListViewCommand(PinModelViewModel obj)
        {
        }

        private int _pinID;
        public int PinId
        {
            get { return _pinID; }
            set { SetProperty(ref _pinID, value); }
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
    }
}
