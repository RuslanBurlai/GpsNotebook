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

        public static readonly BindableProperty ClearSerchBarImageProperty = BindableProperty.Create(
            propertyName: nameof(ClearSerchBarImage),
            returnType: typeof(string),
            declaringType: typeof(CustomSearchBar));

        public string ClearSerchBarImage
        {
            get { return (string)GetValue(ClearSerchBarImageProperty); }
            set { SetValue(ClearSerchBarImageProperty, value); }
        }

        public static readonly BindableProperty TapOnLeftImageCommandProperty = BindableProperty.Create(
            propertyName: nameof(TapOnLeftImageCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomSearchBar));

        public ICommand TapOnLeftImageCommand
        {
            get { return (ICommand)GetValue(TapOnLeftImageCommandProperty); }
            set { SetValue(TapOnLeftImageCommandProperty, value); }
        }

        public static readonly BindableProperty TapOnRightImageCommandProperty = BindableProperty.Create(
            propertyName: nameof(TapOnRightImageCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomSearchBar));

        public ICommand TapOnRightImageCommand
        {
            get { return (ICommand)GetValue(TapOnRightImageCommandProperty); }
            set { SetValue(TapOnRightImageCommandProperty, value); }
        }

        public static readonly BindableProperty TextQueryProperty = BindableProperty.Create(
            propertyName: nameof(TextQuery),
            returnType: typeof(string),
            declaringType: typeof(CustomSearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public string TextQuery
        {
            get { return (string)GetValue(TextQueryProperty); }
            set { SetValue(TextQueryProperty, value); }
        }

        public static readonly BindableProperty IsSearchBarSpanProperty = BindableProperty.Create(
            propertyName: nameof(IsSearchBarSpan),
            returnType: typeof(bool),
            declaringType: typeof(CustomSearchBar));

        public bool IsSearchBarSpan
        {
            get { return (bool)GetValue(IsSearchBarSpanProperty); }
            set { SetValue(IsSearchBarSpanProperty, value); }
        }

        private void ClearSearchEntry(object sender, EventArgs e)
        {
            searchEntry.Text = string.Empty;
        }
    }
}