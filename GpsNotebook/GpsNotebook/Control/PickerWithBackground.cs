using Xamarin.Forms;

namespace GpsNotebook.Control
{
    public class PickerWithBackground : Picker
    {
        public PickerWithBackground()
        {
        }

        public static readonly BindableProperty SelectedBackGroundProperty = BindableProperty.Create(
            propertyName: nameof(SelectedBackGround),
            returnType: typeof(Color),
            declaringType: typeof(PickerWithBackground));

        public Color SelectedBackGround
        {
            get { return (Color)GetValue(SelectedBackGroundProperty); }
            set { SetValue(SelectedBackGroundProperty, value); }
        }
    }
}
