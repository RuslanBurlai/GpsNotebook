using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpsNotebook.Control
{
    public partial class CustomEntry : Grid
    {
        public CustomEntry()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderEntryProperty = BindableProperty.Create(
            propertyName: nameof(HeaderEntry),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string HeaderEntry
        {
            get { return (string)GetValue(HeaderEntryProperty); }
            set { SetValue(HeaderEntryProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty FooterEntryProperty = BindableProperty.Create(
            propertyName: nameof(FooterEntry),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string FooterEntry
        {
            get { return (string)GetValue(FooterEntryProperty); }
            set { SetValue(FooterEntryProperty, value); }
        }

        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            propertyName: nameof(ImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        //public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        //    propertyName: nameof(BorderColor),
        //    returnType: typeof(Color),
        //    declaringType: typeof(CustomEntry));

        //public Color BorderColor
        //{
        //    get { return (Color)GetValue(BorderColorProperty); }
        //    set { SetValue(BorderColorProperty, value); }
        //}

    }
}