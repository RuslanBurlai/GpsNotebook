using GpsNotebook.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Extensions
{
    public static class PinModelExtension
    {
        public static PinViewModel ToPinViewModel(this PinModel pinModel)
        {
            return new PinViewModel
            {
                Id = pinModel.Id,
                UserId = pinModel.UserId,
                PinName = pinModel.PinName,
                PinDescription = pinModel.Description,
                PinLatitude = pinModel.Latitude,
                PinLongitude = pinModel.Longitude,
                PinCategories = pinModel.Categories,
                FavoritPin = pinModel.FavoritPin
            };
        }
    }
}
