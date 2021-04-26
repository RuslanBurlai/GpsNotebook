using GpsNotebook.Models;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Validators;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{

    public class RegisterViewModel : ViewModelBase
    {
        private IUserModelService _userRepository;
        private IPageDialogService _pageDialogService;
        public RegisterViewModel(
            INavigationService navigationService,
            IUserModelService userRepository,
            IPageDialogService pageDialogService) :
            base(navigationService)
        {
            Title = "Create an account";

            _userRepository = userRepository;
            _pageDialogService = pageDialogService;
        }

        #region -- Public properties --

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _userEmail;
        public string UserEmail
        {
            get { return _userEmail; }
            set { SetProperty(ref _userEmail, value); }
        }

        private string _nameError;
        public string NameError
        {
            get { return _nameError; }
            set { SetProperty(ref _nameError, value); }
        }

        private string _emailError;
        public string EmailError
        {
            get { return _emailError; }
            set { SetProperty(ref _emailError, value); }
        }

        private string _entryEmailRightImage;
        public string EntryEmailRightImage
        {
            get { return _entryEmailRightImage; }
            set { SetProperty(ref _entryEmailRightImage, value); }
        }

        private string _entryNameRightImage;
        public string EntryNameRightImage
        {
            get { return _entryNameRightImage; }
            set { SetProperty(ref _entryNameRightImage, value); }
        }

        private ICommand _navigateToRegisterAndPasswordCommand;
        public ICommand NavigateToRegisterAndPasswordCommand =>
            _navigateToRegisterAndPasswordCommand ?? (_navigateToRegisterAndPasswordCommand = new DelegateCommand(OnNavigateToRegisterAndPassword));
        private ICommand _showPreviousViewsCommand;
        public ICommand ShowPreviousViewsCommand =>
            _showPreviousViewsCommand ?? (_showPreviousViewsCommand = new Command(OnShowPreviousViews));

        private ICommand _clearNameEntryCommand;
        public ICommand ClearNameEntryCommand =>
            _clearNameEntryCommand ?? (_clearNameEntryCommand = new Command(OnClearNameEntry));

        private ICommand _clearEmailEntryCommand;
        public ICommand ClearEmailEntryCommand =>
            _clearEmailEntryCommand ?? (_clearEmailEntryCommand = new Command(OnClearEmailEntry));

        private void OnClearEmailEntry()
        {
            UserEmail = string.Empty;
            EmailError = string.Empty;
        }

        private void OnClearNameEntry()
        {
            UserName = string.Empty;
            NameError = string.Empty;
        }

        #endregion

        #region -- Private Helpers --

        private async void OnNavigateToRegisterAndPassword()
        {
            UserModel user = new UserModel
            {
                Email = UserEmail,
                Name = UserName,
            };

            if (Validator.NameValidator(user.Name))
            {
                if (Validator.EmailValidator(user.Email))
                {
                    var newUser = new NavigationParameters();
                    newUser.Add("newUser", user);
                    await NavigationService.NavigateAsync(nameof(RegisterAndPasswordView), newUser);
                }
                else
                {
                    EmailError = "Incorrect email";
                }
            }
            else
            {
                NameError = "Incorrect name";
            }
        }

        private async void OnShowPreviousViews(object obj)
        {
            await NavigationService.NavigateAsync(nameof(LogInOrRegisterView));
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(UserName):
                    {
                        if (UserName != string.Empty)
                        {
                            EntryNameRightImage = "ic_clear";
                        }
                        else
                        {
                            EntryNameRightImage = string.Empty;
                        }
                        break;
                    }
                case nameof(UserEmail):
                    {
                        if (UserEmail != string.Empty)
                        {
                            EntryEmailRightImage = "ic_clear";
                        }
                        else
                        {
                            EntryEmailRightImage = string.Empty;
                        }
                        break;
                    }
            }
        }

        #endregion
    }
}
