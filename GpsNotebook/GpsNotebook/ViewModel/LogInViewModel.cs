using GpsNotebook.Models;
using GpsNotebook.Services.Authorization;
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
        private IPageDialogService _pageDialogService;
        public LogInViewModel(
            IAuthorizationService authorizationService,
            INavigationService navigationService,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Log in";
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

        private bool _showRightImageEmail;
        public bool ShowRightImageEmail
        {
            get { return _showRightImageEmail; }
            set { SetProperty(ref _showRightImageEmail, value); }
        }

        private bool _showRightImagePassword;
        public bool ShowRightImagePassword
        {
            get { return _showRightImagePassword; }
            set { SetProperty(ref _showRightImagePassword, value); }
        }

        private string _imagePathRightIcon;
        public string ImagePathRightIcon
        {
            get { return _imagePathRightIcon; }
            set { SetProperty(ref _imagePathRightIcon, value); }
        }

        private ICommand _clearEmailEntryCommand;
        public ICommand ClearEmailEntryCommand =>
            _clearEmailEntryCommand ?? (_clearEmailEntryCommand = new Command(OnClearEmailEntry));

        private ICommand _hidePasswordCommand;
        public ICommand HidePasswordCommand =>
            _hidePasswordCommand ?? (_hidePasswordCommand = new Command(OnHidePassword));

        private ICommand _onLogInOrRegisterViewCommand;
        public ICommand OnLogInOrRegisterViewCommand =>
            _onLogInOrRegisterViewCommand ?? (_onLogInOrRegisterViewCommand = new Command(OnShowPreviousViews));

        #endregion


        #region -- Private Helpers --

        private void OnHidePassword()
        {
            if (ShowPassword == true)
            {
                ImagePathRightIcon = "ic_eye";
                ShowPassword = false;
            }
            else
            {
                ImagePathRightIcon = "ic_eye_off";
                ShowPassword = true;
            }
        }

        private async void OnShowPreviousViews(object obj)
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
                            ShowRightImageEmail = true;
                        }
                        else
                        {
                            ShowRightImageEmail = false;
                        }
                         break; 
                    }
                case nameof(UserPassword): 
                    {
                        if (UserPassword != string.Empty)
                        {
                            ImagePathRightIcon = "ic_eye";
                            ShowRightImagePassword = true;
                        }
                        else
                        {
                            ShowRightImagePassword = false;
                        }
                        break; 
                    }
            }
        }
        #endregion
    }
}
