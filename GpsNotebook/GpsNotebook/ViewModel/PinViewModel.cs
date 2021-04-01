using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class PinViewModel : ViewModelBase
    {
        public PinViewModel(INavigationService navigationPage) :
            base (navigationPage)
        {
            Title = "List pins";
        }

        #region -- Public properties --

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        #endregion
    }
}
