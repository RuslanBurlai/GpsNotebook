using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        public AddPinViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Add new pin";
        }

        #region --  Public properties --

        private ICommand _savePin;
        public ICommand SavePin =>
            _savePin ?? (_savePin = new DelegateCommand(ExecuteSavePin));

        private async void ExecuteSavePin()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}
