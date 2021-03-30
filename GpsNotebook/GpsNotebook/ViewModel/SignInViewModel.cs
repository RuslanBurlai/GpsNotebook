using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        public SignInViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Sing in";
        }

        #region -- Public properties --

        private ICommand _navigateToSignInCommand;
        public ICommand NavigateToMainListCommand =>
            _navigateToSignInCommand ?? (_navigateToSignInCommand = new DelegateCommand(OnNavigateToSignIn));

        #endregion


        #region -- Private Helpers --
        private void OnNavigateToSignIn()
        {
            NavigationService.NavigateAsync($"{nameof(Map)}");
        }

        #endregion
    }
}
