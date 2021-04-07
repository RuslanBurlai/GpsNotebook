using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Control
{
    public class CustomMap : Map
    {
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
    }
}
