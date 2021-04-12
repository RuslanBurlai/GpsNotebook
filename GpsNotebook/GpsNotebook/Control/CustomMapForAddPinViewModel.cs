using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Control
{
    public class CustomMapForAddPinViewModel : Map
    {
        public CustomMapForAddPinViewModel()
        {
            UiSettings.MyLocationButtonEnabled = true;
            this.MapClicked += CustomMap_MapClicked;
        }

        public static readonly BindableProperty GetPinPositionProperty = BindableProperty.Create(
            propertyName: nameof(GetPinPosition),
            returnType: typeof(Command),
            declaringType: typeof(CustomMapForAddPinViewModel));

        public ICommand GetPinPosition
        {
            get { return (ICommand)GetValue(GetPinPositionProperty); }
            set { SetValue(GetPinPositionProperty, value); }
        }

        private void CustomMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            if(Pins.Count >= 1)
            {
                Pins.Clear();
            }
            else
            {
                GetPinPosition?.Execute(e.Point);
                var pin = new Pin()
                {
                    Position = new Position(e.Point.Latitude, e.Point.Longitude),
                    Label = ""
                };
                this.Pins.Add(pin);
            }
        }
    }
}
