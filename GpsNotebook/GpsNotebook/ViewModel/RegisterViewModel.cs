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

        //private bool _isShowedRightImageName;
        //public bool IsShowedRightImageName
        //{
        //    get { return _isShowedRightImageName; }
        //    set { SetProperty(ref _isShowedRightImageName, value); }
        //}

        //private bool _isShowedRightImageEmail;
        //public bool IsShowedRightImageEmail
        //{
        //    get { return _isShowedRightImageEmail; }
        //    set { SetProperty(ref _isShowedRightImageEmail, value); }
        //}

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
            _navigateToRegisterAndPasswordCommand ?? (_navigateToRegisterAndPasswordCommand = new DelegateCommand(OnNavigateToRegisterAndPassword, CanNavigateToRegisterAndPassword)
            .ObservesProperty<string>(() => UserName)
            .ObservesProperty<string>(() => UserEmail));

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
        }

        private void OnClearNameEntry()
        {
            UserName = string.Empty;
        }

        #endregion

        #region -- Private Helpers --

        private bool CanNavigateToRegisterAndPassword()
        {
            return Validator.AllFieldsIsNullOrEmpty(UserName, UserEmail);
        }

        private async void OnNavigateToRegisterAndPassword()
        {
            UserModel user = new UserModel
            {
                Email = UserEmail,
                Name = UserName,
            };
            await NavigationService.NavigateAsync($"{nameof(RegisterAndPasswordView)}");
            //if (Validator.EmailValidator(UserEmail))
            //{
            //    if (Validator.NameValidator(UserName))
            //    {
            //        await NavigationService.NavigateAsync($"{nameof(LogInView)}");
            //    }
            //    else
            //    {
            //        await _pageDialogService.DisplayAlertAsync("Name error", "name failed", "Ok");
            //    }
            //}
            //else
            //{
            //    await _pageDialogService.DisplayAlertAsync("Email error", "Input correct email.", "Ok");
            //}
        }

        private async void OnShowPreviousViews(object obj)
        {
            await NavigationService.GoBackAsync();
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
                            //IsShowedRightImageName = true;
                            EntryNameRightImage = "ic_clear";
                        }
                        else
                        {
                            //IsShowedRightImageName = false;
                            EntryNameRightImage = string.Empty;
                        }
                        break;
                    }
                case nameof(UserEmail):
                    {
                        if (UserEmail != string.Empty)
                        {
                            EntryEmailRightImage = "ic_clear";
                            //IsShowedRightImageEmail = true;
                        }
                        else
                        {
                            EntryEmailRightImage = string.Empty;
                            //IsShowedRightImageEmail = false;
                        }
                        break;
                    }
            }
        }

        #endregion
    }
}
