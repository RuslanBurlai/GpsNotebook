using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        private IAuthorizationService _authorizationService;
        private IPageDialogService _pageDialogService;
        public SignInViewModel(
            IAuthorizationService authorizationService,
            INavigationService navigationService,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Sing in";
            _authorizationService = authorizationService;
            _pageDialogService = pageDialogService;
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

        private ICommand _navigateToSignUpCommand;
        public ICommand NavigateToSignUpCommand =>
            _navigateToSignUpCommand ?? (_navigateToSignUpCommand = new DelegateCommand(OnNavigateToSignUp));

        #endregion


        #region -- Private Helpers --

        private async void OnNavigateToSignUp()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        private async void OnNavigateToMapTabbedPage()
        {
            UserModel user = new UserModel { Email = UserEmail, Password = UserPassword };
            if (_authorizationService.CheckRegistrationForUser(user))
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("SingIn error", "Your not registered", "Ok");
            }
        }

        private bool CanExecuteNavigateToMapTabbedPage()
        {
            return Validator.AllFieldsIsNullOrEmpty(UserPassword, UserEmail);
        }

        #endregion
    }
}
