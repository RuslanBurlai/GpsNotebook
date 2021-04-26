using GpsNotebook.Models;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class RegisterAndPasswordViewModel : ViewModelBase
    {
        private IUserModelService _userModelServices;
        public RegisterAndPasswordViewModel(
            INavigationService navigationService,
            IUserModelService userModelServices) :
            base(navigationService)
        {
            _userModelServices = userModelServices;
            Title = "Create an account";
        }

        #region -- Public Property --

        private ICommand _onRegisterViewCommand;
        public ICommand OnRegisterViewCommand =>
            _onRegisterViewCommand ?? (_onRegisterViewCommand = new DelegateCommand(OnRegisterView));

        private ICommand _hidePasswordCommand;
        public ICommand HidePasswordCommand =>
            _hidePasswordCommand ?? (_hidePasswordCommand = new Command(OnHidePassword));

        private ICommand _hideConfirmPasswordCommand;
        public ICommand HideConfirmPasswordCommand =>
            _hideConfirmPasswordCommand ?? (_hideConfirmPasswordCommand = new Command(OnHideConfirmPassword));

        private ICommand _navigateToLoginPageCommand;
        public ICommand NavigateToLoginPageCommand =>
            _navigateToLoginPageCommand ?? (_navigateToLoginPageCommand = new DelegateCommand(OnNavigateToLoginPage));


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

        #endregion

        #region -- Private Helpers --

        private UserModel userModel;
        private async void OnNavigateToLoginPage()
        {
            if (Validator.PasswordValidator(UserPassword))
            {
                if (Validator.ConfirmPassowrdValidator(UserPassword, ConfirmPassword))
                {
                    userModel.Password = UserPassword;
                    _userModelServices.AddUser(userModel);
                    await NavigationService.NavigateAsync(nameof(LogInView));
                }
                else
                {
                    PasswordError = "Password missmatch";
                }
            }
            else
            {
                PasswordError = "Password missmatch";
            }
            
        }

        private async void OnRegisterView()
        {
            await NavigationService.NavigateAsync(nameof(RegisterView));
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

        #region -- Overrides --

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
                        }
                        break;
                    }
            }
        }

        #endregion
    }
}
