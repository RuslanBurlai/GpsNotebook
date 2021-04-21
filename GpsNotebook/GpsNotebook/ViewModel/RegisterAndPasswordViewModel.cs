using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class RegisterAndPasswordViewModel : ViewModelBase
    {
        public RegisterAndPasswordViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Create an account";
        }

        #region -- Public Property --

        private ICommand _onRegisterViewCommand;
        public ICommand OnRegisterViewCommand =>
            _onRegisterViewCommand ?? (_onRegisterViewCommand = new Command(OnRegisterView));

        private string _rightImageEntry;
        public string RightImageEntry
        {
            get { return _rightImageEntry; }
            set { SetProperty(ref _rightImageEntry, value); }
        }

        #endregion

        #region -- Private Helpers --

        private async void OnRegisterView(object obj)
        {
            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}
