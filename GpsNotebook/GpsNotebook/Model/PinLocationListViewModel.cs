using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    class PinLocationListViewModel
    {
        public PinLocationListViewModel()
        {
            Pins = new ObservableCollection<PinLocationViewModel>();
            TapOnListViewCell = new Command(TapOnList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PinLocationViewModel> Pins { get; set; }

        public ICommand TapOnListViewCell { protected set; get; }

        private PinLocationViewModel _selectedPin;
        public PinLocationViewModel SelectedPin
        {
            get { return _selectedPin; }
            set { }
        }

        public INavigation Navigation { get; set; }

        private void TapOnList(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
