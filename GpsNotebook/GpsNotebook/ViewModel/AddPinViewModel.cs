using Prism.Navigation;

namespace GpsNotebook.ViewModel
{
    public class AddPinViewModel : ViewModelBase
    {
        public AddPinViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Add new pin";
        }

    }
}
