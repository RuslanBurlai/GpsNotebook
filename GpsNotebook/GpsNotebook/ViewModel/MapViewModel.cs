using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Map with pins";

            AllPins = new ObservableCollection<Pin>();
            AllPins.Add(new Pin {Position = new Position(50.47289, 30.51358), Label = "sdg" });
            AllPins.Add(new Pin {Position = new Position(48.55292, 35.42757), Label = "sdfgh"});
        }

        #region -- Public properties --


        private ObservableCollection<Pin> _allPins;
        public ObservableCollection<Pin> AllPins
        {
            get { return _allPins; }
            set { SetProperty(ref _allPins, value); }
        }

        private ICommand _myLocation;

        public ICommand MyLocation =>
            _myLocation ?? (_myLocation = new DelegateCommand(ExecuteMyLocation));

        private void ExecuteMyLocation()
        {

        }

        #endregion
    }
}
