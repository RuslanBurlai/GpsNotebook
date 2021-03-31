using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class MapTabbedPageViewModel : ViewModelBase
    {
        public MapTabbedPageViewModel(INavigationService navigationService) :
            base (navigationService)
        {
            Title = "Map";
        }

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
    _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private void OnNavigateToAddPin()
        {
            NavigationService.NavigateAsync($"{nameof(AddPin)}");
        }

    }
}
