using GpsNotebook.Services.Permission;
using GpsNotebook.View;
using Plugin.Permissions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class LogInOrRegisteredViewModel : ViewModelBase
    {
        public LogInOrRegisteredViewModel(
            INavigationService navigationService) :
            base(navigationService)
        {
        }

        #region -- Public Property --

        private ICommand _loginCommand;
        public ICommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(OnLogin));

        private ICommand _registerCommand;
        public ICommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(OnRegister));

        #endregion

        #region -- Private Helpers --

        private async void OnRegister()
        {
            await NavigationService.NavigateAsync(nameof(RegisterView));
        }

        private async void OnLogin()
        {

            await NavigationService.NavigateAsync(nameof(LogInView));
        }

        #endregion

    }
}
