using Xamarin.Forms;

namespace GpsNotebook.View
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
