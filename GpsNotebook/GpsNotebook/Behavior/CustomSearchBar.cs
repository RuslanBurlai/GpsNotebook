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
                SearchRequest?.Execute(e.NewTextValue);
            }
        }

        public static readonly BindableProperty SearchRequestProperty = BindableProperty.Create(
            propertyName: nameof(SearchRequest),
            returnType: typeof(Command),
            declaringType: typeof(CustomSearchBar));

        public ICommand SearchRequest
        {
            get { return(ICommand)GetValue(SearchRequestProperty); }
            set { SetValue(SearchRequestProperty, value); }
        }
    }
}
