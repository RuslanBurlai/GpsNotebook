using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        public SignInViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Sing in";
        }

        #region -- Public properties --

        private ICommand _navigateToMapTabbedPageCommand;
        public ICommand NavigateToMapTabbedPageCommand =>
            _navigateToMapTabbedPageCommand ?? (_navigateToMapTabbedPageCommand = new DelegateCommand(OnNavigateToMapTabbedPage));

        #endregion


        #region -- Private Helpers --
        private void OnNavigateToMapTabbedPage()
        {
            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MapTabbedPage)}");
        }

        #endregion
    }
}
