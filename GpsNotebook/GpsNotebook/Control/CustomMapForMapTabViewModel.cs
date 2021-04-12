using System.Collections.Generic;
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
            //propertyChanged: OnListOfPinsChanged);



        public IEnumerable<Pin> ListOfPins
        {
            get { return (IEnumerable<Pin>)GetValue(ListOfPinsProperty); }
            set { SetValue(ListOfPinsProperty, value); }
        }

        private static void OnListOfPinsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMapForMapTabViewModel;

            //if (newValue != null)
            //    map.ListOfPins = (IList<Pin>)newValue;
        }

        //use OnListOfPinsChanged and remove OnPropertyChanged
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(ListOfPins))
            {
                Pins.Clear();
                foreach (var item in ListOfPins)
                {
                    Pins.Add(item);
                }
            }
        }
    }
}
