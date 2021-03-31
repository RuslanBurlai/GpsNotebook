using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{

    public class SignUpViewModel : ViewModelBase
    {
        public SignUpViewModel(INavigationService navigationService) :
            base (navigationService)  
        {
            Title = "Sign Up";
        }

        private ICommand _navigateToSignInCommand;
        public ICommand NavigateToSignInCommand =>
            _navigateToSignInCommand ?? (_navigateToSignInCommand = new DelegateCommand(OnNavigateToSignIn));

        private void OnNavigateToSignIn()
        {
            throw new NotImplementedException();
        }
    }
}
