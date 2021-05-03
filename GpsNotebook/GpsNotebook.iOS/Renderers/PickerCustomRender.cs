using GpsNotebook.Control;
using GpsNotebook.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(PickerWithBackground), typeof(PickerCustomRender))]
namespace GpsNotebook.iOS.Renderers
{
    public class PickerCustomRender : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            UITextField textField = Control;
            UIPickerView pickerView = textField.InputView as UIPickerView;
            //pickerView.BackgroundColor = 
        }
    }
}