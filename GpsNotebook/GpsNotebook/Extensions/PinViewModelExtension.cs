using GpsNotebook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Extensions
{
    public static class PinViewModelExtension
    {
        public static Pin ToPin(this PinViewModel pinViewModel)
        {
            return new Pin
            {
                Label = pinViewModel.PinName,
                Position = new Position(pinViewModel.PinLatitude, pinViewModel.PinLongitude),
                Tag = pinViewModel.PinDescription
            };
        }

        public static PinModel ToPinModel(this PinViewModel pinViewModel)
        {
            return new PinModel
            {
                Id = pinViewModel.Id,
                UserId = pinViewModel.UserId,
                PinName = pinViewModel.PinName,
                Description = pinViewModel.PinDescription,
                Latitude = pinViewModel.PinLatitude,
                Longitude = pinViewModel.PinLongitude,
                Categories = pinViewModel.PinCategories,
                FavoritPin = pinViewModel.FavoritPin,
            };
        }
    }
}
