using Xamarin.Forms;

namespace GpsNotebook.View
{
    public class BaseTabbedPage : TabbedPage
    {
        public BaseTabbedPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        public static readonly BindableProperty TappedTabBackGroundProperty = BindableProperty.Create(
            propertyName: nameof(TappedTabBackGround),
            returnType: typeof(Color),
            declaringType: typeof(BaseTabbedPage));

        public Color TappedTabBackGround 
        {
            get { return (Color)GetValue(TappedTabBackGroundProperty); }
            set { SetValue(TappedTabBackGroundProperty, value); }
        }

        public static readonly BindableProperty TappedTabColorProperty = BindableProperty.Create(
            propertyName: nameof(TappedTabColor),
            returnType: typeof(Color),
            declaringType: typeof(BaseTabbedPage));
        public Color TappedTabColor
        {
            get { return (Color)GetValue(TappedTabColorProperty); }
            set { SetValue(TappedTabColorProperty, value); }
        }
    }
}
