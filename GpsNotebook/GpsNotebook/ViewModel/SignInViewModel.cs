using GpsNotebook.Helpers;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
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

        private ICommand _navigateToMapTabbedPageCommand;
        public ICommand NavigateToMapTabbedPageCommand =>
            _navigateToMapTabbedPageCommand ?? (_navigateToMapTabbedPageCommand = new DelegateCommand(OnNavigateToMapTabbedPage, CanExecuteNavigateToMapTabbedPage)
            .ObservesProperty<string>(() => UserEmail)
            .ObservesProperty<string>(() => UserPassword));

        private async void OnNavigateToMapTabbedPage()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
        }

        private bool CanExecuteNavigateToMapTabbedPage()
        {
            return EntryChecker.EntryIsNullOrEmpty(UserPassword, UserEmail);
        }

        private ICommand _navigateToSignUpCommand;
        public ICommand NavigateToSignUpCommand =>
            _navigateToSignUpCommand ?? (_navigateToSignUpCommand = new DelegateCommand(OnNavigateToSignUp));

        private async void OnNavigateToSignUp()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        #endregion


        #region -- Private Helpers --

        #endregion
    }
}
