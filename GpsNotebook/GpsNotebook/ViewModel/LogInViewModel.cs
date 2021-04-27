using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        private IAuthorizationService _authorizationService;
        private IUserModelService _userModelService;
        private IPageDialogService _pageDialogService;
        public LogInViewModel(
            IAuthorizationService authorizationService,
            INavigationService navigationService,
            IUserModelService userModelService,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Log in";

            _authorizationService = authorizationService;
            _userModelService = userModelService;
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

        private bool _showPassword;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }

        private string _emailError;
        public string EmailError
        {
            get { return _emailError; }
            set { SetProperty(ref _emailError, value); }
        }

        private string _passwordError;
        public string PasswordError
        {
            get { return _passwordError; }
            set { SetProperty(ref _passwordError, value); }
        }

        private string _passwordEntryRightImage;
        public string PasswordEntryRightImage
        {
            get { return _passwordEntryRightImage; }
            set { SetProperty(ref _passwordEntryRightImage, value); }
        }

        private string _emailEntryRightImage;
        public string EmailEntryRightImage
        {
            get { return _emailEntryRightImage; }
            set { SetProperty(ref _emailEntryRightImage, value); }
        }

        private ICommand _clearEmailEntryCommand;
        public ICommand ClearEmailEntryCommand =>
            _clearEmailEntryCommand ?? (_clearEmailEntryCommand = new Command(OnClearEmailEntry));

        private ICommand _hidePasswordCommand;
        public ICommand HidePasswordCommand =>
            _hidePasswordCommand ?? (_hidePasswordCommand = new Command(OnHidePassword));

        private ICommand _logInOrRegisterViewCommand;
        public ICommand LogInOrRegisterViewCommand =>
            _logInOrRegisterViewCommand ?? (_logInOrRegisterViewCommand = new Command(OnLogInOrRegisterView));

        private ICommand _navigateToMapTabbedPageCommand;
        public ICommand NavigateToMapTabbedPageCommand =>
            _navigateToMapTabbedPageCommand ?? (_navigateToMapTabbedPageCommand = new DelegateCommand(OnNavigateToMapTabbedPage));

        #endregion

        #region -- Private Helpers --

        private async void OnNavigateToMapTabbedPage()
        {
            if (Validator.AllFieldsIsNullOrEmpty(UserEmail, UserPassword))
            {
                if (_userModelService.GetUserId(UserEmail, UserPassword) != 0)
                {
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
                }
                else
                {
                    EmailError = "Email incorrect";
                    PasswordError = "Password incorrect";
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "Fill all fields.", "Ok");
            }
        }

        private void OnHidePassword()
        {
            if (ShowPassword == true)
            {
                PasswordEntryRightImage = "ic_eye";
                ShowPassword = false;
            }
            else
            {
                PasswordEntryRightImage = "ic_eye_off";
                ShowPassword = true;
            }
        }

        private async void OnLogInOrRegisterView()
        {
            await NavigationService.GoBackAsync();
        }

        private void OnClearEmailEntry()
        {
            UserEmail = string.Empty;
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(UserEmail): 
                    {
                        if (UserEmail != string.Empty)
                        {
                            EmailEntryRightImage = "ic_clear";
                        }
                        else
                        {
                            EmailEntryRightImage = string.Empty;
                        }
                         break; 
                    }
                case nameof(UserPassword): 
                    {
                        if (UserPassword != string.Empty)
                        {
                            PasswordEntryRightImage = "ic_eye";
                        }
                        else
                        {
                            PasswordEntryRightImage = string.Empty;
                        }
                        break; 
                    }
            }
        }
        #endregion
    }
}
