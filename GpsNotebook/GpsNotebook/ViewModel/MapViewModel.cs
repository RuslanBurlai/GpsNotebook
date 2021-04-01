using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Map with pins";
        }

        #region -- Public properties --

        private ICommand _myLocation;
        public ICommand MyLocation =>
            _myLocation ?? (_myLocation = new DelegateCommand(ExecuteMyLocation));

        private void ExecuteMyLocation()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
