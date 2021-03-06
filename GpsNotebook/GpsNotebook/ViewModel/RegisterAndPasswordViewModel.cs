using GpsNotebook.Models;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class RegisterAndPasswordViewModel : ViewModelBase
    {
        private IUserModelService _userModelServices;
        private IPageDialogService _pageDialogService;

        public RegisterAndPasswordViewModel(
            INavigationService navigationService,
            IUserModelService userModelServices,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Create an account";
            _userModelServices = userModelServices;
            _pageDialogService = pageDialogService;
        }

        #region -- Public Property --

        private ICommand _registerViewCommand;
        public ICommand RegisterViewCommand =>
            _registerViewCommand ?? (_registerViewCommand = new DelegateCommand(OnRegisterView));

        private ICommand _hidePasswordCommand;
        public ICommand HidePasswordCommand =>
            _hidePasswordCommand ?? (_hidePasswordCommand = new Command(OnHidePassword));

        private ICommand _hideConfirmPasswordCommand;
        public ICommand HideConfirmPasswordCommand =>
            _hideConfirmPasswordCommand ?? (_hideConfirmPasswordCommand = new Command(OnHideConfirmPassword));

        private ICommand _navigateToLoginPageCommand;
        public ICommand NavigateToLoginPageCommand =>
            _navigateToLoginPageCommand ?? (_navigateToLoginPageCommand = new DelegateCommand(OnNavigateToLoginPage));

        private UserModel userModel;

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

        private string _entryPasswordRightIcon;
        public string EntryPasswordRightIcon
        {
            get { return _entryPasswordRightIcon; }
            set { SetProperty(ref _entryPasswordRightIcon, value); }
        }

        private string _entryConfirmPasswordRightIcon;
        public string EntryConfirmPasswordRightIcon
        {
            get { return _entryConfirmPasswordRightIcon; }
            set { SetProperty(ref _entryConfirmPasswordRightIcon, value); }
        }

        private string _confirmPasswordError;
        public string ConfirmPasswordError
        {
            get { return _confirmPasswordError; }
            set { SetProperty(ref _confirmPasswordError, value); }
        }

        private string _passwordError;
        public string PasswordError
        {
            get { return _passwordError; }
            set { SetProperty(ref _passwordError, value); }
        }

        private bool _isPasswordHideing;
        public bool IsPasswordHideing
        {
            get { return _isPasswordHideing; }
            set { SetProperty(ref _isPasswordHideing, value); }
        }

        private bool _isConfirmPasswordHiding;
        public bool IsConfirmPasswordHiding
        {
            get { return _isConfirmPasswordHiding; }
            set { SetProperty(ref _isConfirmPasswordHiding, value); }
        }

        private Color _errorColorBorder;
        public Color ErrorColorBorder 
        {
            get { return _errorColorBorder; }
            set { SetProperty(ref _errorColorBorder, value); }
        }

        private Color _entryPasswordBorder;
        public Color EntryPasswordBorder
        {
            get { return _entryPasswordBorder; }
            set { SetProperty(ref _entryPasswordBorder, value); }
        }

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ErrorColorBorder = Color.FromHex("#D7DDE8");
            EntryPasswordBorder = Color.FromHex("#D7DDE8");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue<UserModel>("newUser", out UserModel newUser))
            {
                userModel = newUser;
            }
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(UserPassword):
                    {
                        if (UserPassword != string.Empty)
                        {
                            EntryPasswordRightIcon = "ic_eye";
                        }
                        else
                        {
                            EntryPasswordRightIcon = string.Empty;
                            PasswordError = string.Empty;
                            EntryPasswordBorder = Color.FromHex("#D7DDe8");
                        }
                        break;
                    }

                case nameof(ConfirmPassword):
                    {
                        if (ConfirmPassword != string.Empty)
                        {
                            EntryConfirmPasswordRightIcon = "ic_eye";
                        }
                        else
                        {
                            EntryConfirmPasswordRightIcon = string.Empty;
                            ConfirmPasswordError = string.Empty;
                            ErrorColorBorder = Color.FromHex("#D7DDe8");
                        }
                        break;
                    }
            }
        }

        #endregion

        #region -- Private Helpers --


        private async void OnNavigateToLoginPage()
        {
            if (Validator.AllFieldsIsNullOrEmpty(UserPassword, ConfirmPassword))
            {
                if (Validator.PasswordValidator(UserPassword))
                {
                    if (Validator.ConfirmPassowrdValidator(UserPassword, ConfirmPassword))
                    {
                        userModel.Password = UserPassword;
                        _userModelServices.AddUser(userModel);
                        NavigationParameters userEmail = new NavigationParameters();
                        userEmail.Add("UserEmail", userModel.Email);
                        await NavigationService.NavigateAsync(nameof(LogInView), userEmail);
                    }
                    else
                    {
                        ConfirmPasswordError = "Password missmatch";
                        ErrorColorBorder = Color.FromHex("#F24545");
                    }
                }
                else
                {
                    PasswordError = "Password not correct";
                    EntryPasswordBorder = Color.FromHex("#F24545");
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "Fill all fields.", "Ok");
            }

        }

        private async void OnRegisterView()
        {
            await NavigationService.GoBackAsync();
        }

        private void OnHidePassword()
        {
            if (IsPasswordHideing != true)
            {
                EntryPasswordRightIcon = "ic_eye_off";
                IsPasswordHideing = true;
            }
            else
            {
                EntryPasswordRightIcon = "ic_eye";
                IsPasswordHideing = false;
            }
        }

        private void OnHideConfirmPassword(object obj)
        {
            if (IsConfirmPasswordHiding != true)
            {
                EntryConfirmPasswordRightIcon = "ic_eye_off";
                IsConfirmPasswordHiding = true;
            }
            else
            {
                EntryConfirmPasswordRightIcon = "ic_eye";
                IsConfirmPasswordHiding = false;
            }
        }

        #endregion
    }
}
