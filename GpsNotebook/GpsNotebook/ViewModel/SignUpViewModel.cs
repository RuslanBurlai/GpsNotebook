using GpsNotebook.Models;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{

    public class SignUpViewModel : ViewModelBase
    {
        private IUserModelService _userRepository;
        private IPageDialogService _pageDialogService;
        public SignUpViewModel(
            INavigationService navigationService,
            IUserModelService userRepository,
            IPageDialogService pageDialogService) :
            base (navigationService)  
        {
            Title = "Sign Up";

            _userRepository = userRepository;
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

        #endregion

        #region -- Private Helpers --

        private bool CanExecuteNavigateToSignIn()
        {
            return Validator.AllFieldsIsNullOrEmpty(UserName, UserEmail, UserPassword, ConfirmPassword);
        }

        private async void OnNavigateToSignIn()
        {
            UserModel user = new UserModel 
            { 
                Email = UserEmail, 
                Name =  UserName, 
                Password = UserPassword 
            };

            if(Validator.EmailValidator(UserEmail))
            {
                if(Validator.PasswordValidator(UserPassword))
                {
                    if(Validator.ConfirmPassowrdValidator(UserPassword, ConfirmPassword))
                    {
                        _userRepository.AddUser(user);
                        await NavigationService.NavigateAsync($"{nameof(LogInView)}");
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Password not confirmed.", "Password and confirm password not equal.", "Ok");
                    }
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Password not correct.", "Password must contain at least numbers and uppercase characters.", "Ok");
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Email error", "Input correct email.", "Ok");
            }
        }

        #endregion
    }
}
