using GpsNotebook.Helpers;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{

    public class SignUpViewModel : ViewModelBase
    {
        public SignUpViewModel(INavigationService navigationService) :
            base (navigationService)  
        {
            Title = "Sign Up";
        }

        #region -- Public properties --

        private string _userEmail;
        public string UserEmail
        {

            get { return _userEmail; }
            set { SetProperty(ref _userEmail, value); }
        }

        private string _userPassword; 
        public string UserPassword
        { 
            get { return _userPassword; }
            set { SetProperty(ref _userPassword, value); } 
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private ICommand _navigateToSignInCommand;
        public ICommand NavigateToSignInCommand =>
            _navigateToSignInCommand ?? (_navigateToSignInCommand = new DelegateCommand(OnNavigateToSignIn, CanExecuteNavigateToSignIn)
            .ObservesProperty<string>(() => UserName)
            .ObservesProperty<string>(() => UserEmail)
            .ObservesProperty<string>(() => UserPassword)
            .ObservesProperty<string>(() => ConfirmPassword));

        private bool CanExecuteNavigateToSignIn()
        {
            return EntryChecker.EntryIsNullOrEmpty(UserName, UserEmail, UserPassword, ConfirmPassword);
        }

        private async void OnNavigateToSignIn()
        {
            await NavigationService.NavigateAsync($"{nameof(SignInView)}");
        }

        #endregion 
    }
}
