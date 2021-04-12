using GpsNotebook.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Extensions
{
    public static class Extension
    {
        //remove
        public static ObservableCollection<Pin> PinToMapTabView(this PinLocation pinLocation, IEnumerable<PinLocation> pinLocations)
        {
            var pins = new ObservableCollection<Pin>();

            foreach (var item in pinLocations)
            {
                var pin = new Pin
                {
                    Label = item.PinName,
                    Position = new Position(item.Latitude, item.Longitude)
                };
                pins.Add(pin);
            }
            return pins;
        }

        public static Pin ToPinModel(this PinLocation pinLocation)
        {
            return new Pin
            {
                Label = pinLocation.PinName,
                Position = new Position(pinLocation.Latitude, pinLocation.Longitude)
            };
        }
    }
}
