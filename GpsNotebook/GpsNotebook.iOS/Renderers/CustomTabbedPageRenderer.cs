using CoreGraphics;
using GpsNotebook.iOS.Renderers;
using GpsNotebook.View;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace GpsNotebook.iOS.Renderers
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        public UIImage imageWithColor(CGSize size)
        {
            var p = Element as BaseTabbedPage;

            CGRect rect = new CGRect(0, 0, size.Width, size.Height);
            UIGraphics.BeginImageContext(size);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.SetFillColor(p.TappedTabBackGround.ToCGColor());
                context.FillRect(rect);
            }

            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var p = Element as BaseTabbedPage;

            CGSize size = new CGSize(TabBar.Frame.Width / TabBar.Items.Length, TabBar.Frame.Height);
            //Background Color
            UITabBar.Appearance.SelectionIndicatorImage = imageWithColor(size);
            //Normal title Color
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.Red }, UIControlState.Normal);
            //Selected title Color
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = p.TappedTabColor.ToUIColor()}, UIControlState.Selected);
        }
    }
}
