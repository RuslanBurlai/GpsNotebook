using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.Control
{
    public partial class CustomEntryWithLabel : Grid
    {
        public CustomEntryWithLabel()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderEntryProperty = BindableProperty.Create(
            propertyName: nameof(HeaderEntry),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithLabel));

        public string HeaderEntry
        {
            get { return (string)GetValue(HeaderEntryProperty); }
            set { SetValue(HeaderEntryProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithLabel));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty FooterEntryProperty = BindableProperty.Create(
            propertyName: nameof(FooterEntry),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithLabel));

        public string FooterEntry
        {
            get { return (string)GetValue(FooterEntryProperty); }
            set { SetValue(FooterEntryProperty, value); }
        }

        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            propertyName: nameof(ImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryWithLabel));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntryWithLabel));

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty ClearOrHideTextProperty = BindableProperty.Create(
            propertyName: nameof(ClearOrHideText),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomEntryWithLabel));

        public ICommand ClearOrHideText
        {
            get { return (ICommand)GetValue(ClearOrHideTextProperty); }
            set { SetValue(ClearOrHideTextProperty, value); }
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntryWithLabel));

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
    }
}