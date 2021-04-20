using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using GpsNotebook.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(NoUnderlineEntry))]
namespace GpsNotebook.Droid.Renderers
{
    public class NoUnderlineEntry : EntryRenderer
    {
        public NoUnderlineEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }

            // add border to entry.
            //if (e.OldElement == null)
            //{
            //    var nativeEditText = (EditText)Control;
            //    var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            //    shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
            //    shape.Paint.SetStyle(Paint.Style.Stroke);
            //    nativeEditText.Background = shape;
            //    GradientDrawable gd = new GradientDrawable();
            //    gd.SetColor(Android.Graphics.Color.White);
            //    //gd.SetCornerRadius(10);
            //    gd.SetStroke(2, Android.Graphics.Color.LightGray);
            //    nativeEditText.SetBackground(gd);
            //}
        }
    }
}