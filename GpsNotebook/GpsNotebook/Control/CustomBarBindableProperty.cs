using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.Control
{
    public class CustomBarBindableProperty : Grid
    {
        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            propertyName: nameof(ImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomBarBindableProperty));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly BindableProperty NavBarTitleProperty = BindableProperty.Create(
    propertyName: nameof(NavBarTitle),
    returnType: typeof(string),
    declaringType: typeof(CustomBarBindableProperty));

        public string NavBarTitle
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly BindableProperty GoBackProperty = BindableProperty.Create(
            propertyName: nameof(GoBack),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomBarBindableProperty));

        public ICommand GoBack
        {
            get { return (ICommand)GetValue(GoBackProperty); }
            set { SetValue(GoBackProperty, value); }
        }
    }
}
