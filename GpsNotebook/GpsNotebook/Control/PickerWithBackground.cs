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

        public static readonly BindableProperty SelectedThemeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTheme),
            returnType: typeof(string),
            declaringType: typeof(PickerWithBackground));

        public string SelectedTheme
        {
            get { return (string)GetValue(SelectedThemeProperty); }
            set { SetValue(SelectedThemeProperty, value); }
        }
    }
}
