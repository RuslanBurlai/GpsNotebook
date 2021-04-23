using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpsNotebook.Control
{
    public partial class CustomSearchBar : Grid
    {
        public CustomSearchBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty RightSideImagePathProperty = BindableProperty.Create(
            propertyName: nameof(RightSideImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomSearchBar));

        public string RightSideImagePath
        {
            get { return (string)GetValue(RightSideImagePathProperty); }
            set { SetValue(RightSideImagePathProperty, value); }
        }

        public static readonly BindableProperty LeftSideImagePathProperty = BindableProperty.Create(
            propertyName: nameof(LeftSideImagePath),
            returnType: typeof(string),
            declaringType: typeof(CustomSearchBar));

        public string LeftSideImagePath
        {
            get { return (string)GetValue(LeftSideImagePathProperty); }
            set { SetValue(LeftSideImagePathProperty, value); }
        }
    }
}