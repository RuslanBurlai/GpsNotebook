using GpsNotebook.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Extensions
{
    public static class PinModelExtension
    {
        //remove
        public static ObservableCollection<Pin> PinToMapTabView(this PinModel pinLocation, IEnumerable<PinModel> pinLocations)
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

        public static Pin ToPinModel(this PinModel pinLocation)
        {
            return new Pin
            {
                Label = pinLocation.PinName,
                Position = new Position(pinLocation.Latitude, pinLocation.Longitude)
            };
        }
    }
}
