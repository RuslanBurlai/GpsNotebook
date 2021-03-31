using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GpsNotebook.ViewModel
{
    public class PinViewModel : ViewModelBase
    {
        public PinViewModel(INavigationService navigationPage) :
            base (navigationPage)
        {
            Title = "Pin";
        }
    }
}
