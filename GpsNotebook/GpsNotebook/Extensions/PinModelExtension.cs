using GpsNotebook.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Extensions
{
    public static class PinModelExtension
    {
        public static Pin ToPin(this PinModel pinModel)
        {
            return new Pin
            {
                Label = pinModel.PinName,
                Position = new Position(pinModel.Latitude, pinModel.Longitude)
            };
        }

        public static PinModelViewModel ToPinViewModel(this PinModel pinModel)
        {
            return new PinModelViewModel
            {
                PinId = pinModel.Id,
                PinName = pinModel.PinName,
                PinDescription = pinModel.Description,
                PinLatitude = pinModel.Latitude,
                PinLongitude = pinModel.Longitude
            };
        }
    }
}
