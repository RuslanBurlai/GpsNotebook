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

        private Color _entryNameColor;
        public Color EntryNameColor
        {
            get { return _entryNameColor; }
            set { SetProperty(ref _entryNameColor, value); }
        }

        private Color _entryEmailColor;
        public Color EntryEmailColor
        {
            get { return _entryEmailColor; }
            set { SetProperty(ref _entryEmailColor, value); }
        }

        private ICommand _navigateToRegisterAndPasswordCommand;
        public ICommand NavigateToRegisterAndPasswordCommand =>
            _navigateToRegisterAndPasswordCommand ?? (_navigateToRegisterAndPasswordCommand = new DelegateCommand(OnNavigateToRegisterAndPassword));

        private ICommand _logInOrRegisterViewCommand;
        public ICommand LogInOrRegisterViewCommand =>
            _logInOrRegisterViewCommand ?? (_logInOrRegisterViewCommand = new Command(OnLogInOrRegisterView));

        private ICommand _clearNameEntryCommand;
        public ICommand ClearNameEntryCommand =>
            _clearNameEntryCommand ?? (_clearNameEntryCommand = new Command(OnClearNameEntry));

        private ICommand _clearEmailEntryCommand;
        public ICommand ClearEmailEntryCommand =>
            _clearEmailEntryCommand ?? (_clearEmailEntryCommand = new Command(OnClearEmailEntry));

        #endregion

        #region -- Private Helpers --

        private void OnClearEmailEntry()
        {
            UserEmail = string.Empty;
            EmailError = string.Empty;
            EntryEmailColor = Color.FromHex("#D7DDe8");
        }

        private void OnClearNameEntry()
        {
            UserName = string.Empty;
            NameError = string.Empty;
            EntryNameColor = Color.FromHex("#D7DDe8");
        }

        private bool CanNavigateToRegisterAndPassword()
        {
            return Validator.AllFieldsIsNullOrEmpty(UserName, UserEmail);
        }

        private async void OnNavigateToRegisterAndPassword()
        {
            if (Validator.AllFieldsIsNullOrEmpty(UserName, UserEmail))
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
                        EntryEmailColor = Color.FromHex("#F24545");
                        EmailError = "Incorrect email";
                    }
                }
                else
                {
                    EntryNameColor = Color.FromHex("#F24545");
                    NameError = "Incorrect name";
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error", "Fill all fields.", "Ok");
            }
        }

        private async void OnLogInOrRegisterView()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion

        #region -- Overrides --
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            EntryEmailColor = Color.FromHex("#D7DDe8");
            EntryNameColor = Color.FromHex("#D7DDe8");
        }

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
