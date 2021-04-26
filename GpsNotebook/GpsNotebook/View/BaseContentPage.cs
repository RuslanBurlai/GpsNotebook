using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GpsNotebook.View
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
