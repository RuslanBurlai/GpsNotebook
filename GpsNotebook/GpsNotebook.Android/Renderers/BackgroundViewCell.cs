using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using GpsNotebook.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ViewCell), typeof(BackgroundViewCell))]
namespace GpsNotebook.Droid.Renderers
{
    public class BackgroundViewCell : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);

            _selected = false;
            //_unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        //protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    base.OnCellPropertyChanged(sender, args);

        //    if (args.PropertyName == "IsSelected")
        //    {
        //        _selected = !_selected;

        //        if (_selected)
        //        {
        //            var ViewCell = sender as ViewCell;
        //            _cellCore.SetBackgroundColor(ViewCell.SelectedBackgroundColor.ToAndroid());
        //        }
        //        else
        //        {
        //            _cellCore.SetBackground(_unselectedBackground);
        //        }
        //    }
        //}
    }
}