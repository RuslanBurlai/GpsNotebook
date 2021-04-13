using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.Behavior
{
    public class CustomSearchBar : SearchBar
    {
        //use EventToCommandBehavior instead of this custom serach bar, remove this file
        public CustomSearchBar()
        {
            this.TextChanged += CustomSearchBar_TextChanged;
        }

        private void CustomSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue != null)
            {
                GetSearchRequest?.Execute(e.NewTextValue);
            }
            else
            {
                GetSearchRequest.Execute(e.OldTextValue);
            }
        }

        public static readonly BindableProperty GetSearchRequestProperty = BindableProperty.Create(
            propertyName: nameof(GetSearchRequest),
            returnType: typeof(Command),
            declaringType: typeof(CustomSearchBar));

        public ICommand GetSearchRequest
        {
            get { return(ICommand)GetValue(GetSearchRequestProperty); }
            set { SetValue(GetSearchRequestProperty, value); }
        }
    }
}
