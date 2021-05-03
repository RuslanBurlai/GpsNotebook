using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Control
{
    //try to do one control
    public class CustomMapForMapTabViewModel : Map
    {
        public CustomMapForMapTabViewModel()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }

        public static readonly BindableProperty ListOfPinsProperty = BindableProperty.Create(
            propertyName: nameof(ListOfPins),
            returnType: typeof(IEnumerable<Pin>),
            declaringType: typeof(CustomMapForAddPinViewModel));

        public IEnumerable<Pin> ListOfPins
        {
            get { return (IEnumerable<Pin>)GetValue(ListOfPinsProperty); }
            set { SetValue(ListOfPinsProperty, value); }
        }

        public static readonly BindableProperty CameraOnSelectedPinProperty = BindableProperty.Create(
            propertyName: nameof(CameraOnSelectedPin),
            returnType: typeof(MapSpan),
            declaringType: typeof(CustomMapForMapTabViewModel),
            propertyChanged: OnCameraOnSelectedPinChanged);

        private static void OnCameraOnSelectedPinChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cameraOnPin = bindable as CustomMapForMapTabViewModel;
            var mapSpan = newValue as MapSpan;

            if(newValue != null)
            {
                cameraOnPin.MoveToRegion(mapSpan);
            }
        }

        public MapSpan CameraOnSelectedPin
        {
            get { return (MapSpan)GetValue(CameraOnSelectedPinProperty); }
            set { SetValue(CameraOnSelectedPinProperty, value); }
        }

        //use OnListOfPinsChanged and remove OnPropertyChanged
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(ListOfPins))
            {
                if (ListOfPins != null)
                {
                    Pins.Clear();
                }
                foreach (var item in ListOfPins)
                {
                    Pins.Add(item);
                }
            }
        }
    }
}
