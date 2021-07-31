using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpsNotebook.Control
{
    public partial class CustomNavigationBar : Grid
    {
        public CustomNavigationBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            propertyName: nameof(ImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationBar));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly BindableProperty NavBarTitleProperty = BindableProperty.Create(
            propertyName: nameof(NavBarTitle),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationBar));

        public string NavBarTitle
        {
            get { return (string)GetValue(NavBarTitleProperty); }
            set { SetValue(NavBarTitleProperty, value); }
        }

        public static readonly BindableProperty GoBackCommandProperty = BindableProperty.Create(
            propertyName: nameof(GoBackCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomNavigationBar));

        public ICommand GoBackCommand
        {
            get { return (ICommand)GetValue(GoBackCommandProperty); }
            set { SetValue(GoBackCommandProperty, value); }
        }

        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(RightImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationBar));

        public string RightImageSource
        {
            get { return (string)GetValue(RightImageSourceProperty); }
            set { SetValue(RightImageSourceProperty, value); }
        }

        public static readonly BindableProperty RightImageCommandProperty = BindableProperty.Create(
            propertyName: nameof(RightImageCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomNavigationBar));

        public ICommand RightImageCommand
        {
            get { return (ICommand)GetValue(RightImageCommandProperty); }
            set { SetValue(RightImageCommandProperty, value); }
        }
    }
}