using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Control
{
    public class CustomMap : Map
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
            this.MapClicked += CustomMap_MapClicked;
        }

        public static readonly BindableProperty ListOfPinsProperty = BindableProperty.Create(
            propertyName: nameof(ListOfPins),
            returnType: typeof(IEnumerable<Pin>),
            declaringType: typeof(CustomMap),
            propertyChanged: OnListOfPinsChanged);

        public IEnumerable<Pin> ListOfPins
        {
            get { return (IEnumerable<Pin>)GetValue(ListOfPinsProperty); }
            set { SetValue(ListOfPinsProperty, value); }
        }

        private static void OnListOfPinsChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public static readonly BindableProperty GetPinPositionProperty = BindableProperty.Create(
            propertyName: nameof(GetPinPosition),
            returnType: typeof(Command),
            declaringType: typeof(CustomMap));

        public ICommand GetPinPosition
        {
            get { return (ICommand)GetValue(GetPinPositionProperty); }
            set { SetValue(GetPinPositionProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == nameof(ListOfPins))
            {
                foreach (var item in ListOfPins)
                {
                    Pins.Add(item);
                }
            }
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

        private void Pin_Clicked(object sender, System.EventArgs e)
        {
        }
    }
}
