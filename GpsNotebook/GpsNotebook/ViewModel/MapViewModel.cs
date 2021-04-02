using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;



namespace GpsNotebook.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Map with pins";
            
        }

        #region -- Public properties --

        private ICommand _myLocation;

        public ICommand MyLocation =>
            _myLocation ?? (_myLocation = new DelegateCommand(ExecuteMyLocation));

        private void ExecuteMyLocation()
        {
            //GetCurrentLocation();
        }

        #endregion


        //CancellationTokenSource cts;

        //async Task GetCurrentLocation()
        //{
        //    try
        //    {
        //        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        //        cts = new CancellationTokenSource();
        //        var location = await Geolocation.GetLocationAsync(request, cts.Token);

        //        if (location != null)
        //        {
        //            //await Map.OpenAsync(location.Latitude, location.Longitude);
        //            Position position = new Position(location.Latitude, location.Longitude);
        //            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
        //            //Map map = new Map(mapSpan);
        //            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
        //        }


        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        // Handle not supported on device exception
        //    }
        //    catch (FeatureNotEnabledException fneEx)
        //    {
        //        // Handle not enabled on device exception
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        // Handle permission exception
        //    }
        //    catch (Exception ex)
        //    {
        //        // Unable to get location
        //    }
        //}
    }

    //public class CustomMap : Xamarin.Forms.Maps.Map
    //{
    //    public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items", typeof(IEnumerable<CustomPin>), typeof(CustomMap),
    //        null, propertyChanged: OnItemsChanged);

    //    public IEnumerable<CustomPin> Items
    //    {
    //        get { return (IEnumerable<CustomPin>)GetValue(ItemsProperty); }
    //        set { SetValue(ItemsProperty, value); }
    //    }

    //    static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    //    {
    //        var map = bindable as CustomMap;

    //        if (oldValue is INotifyCollectionChanged)
    //            (oldValue as INotifyCollectionChanged).CollectionChanged -= map.OnCollectionChanged;
    //        if (newValue is INotifyCollectionChanged)
    //            (newValue as INotifyCollectionChanged).CollectionChanged += map.OnCollectionChanged;

    //        map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    //        if (newValue != null)
    //            map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)newValue));
    //    }

    //    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //    {
    //        if (e.Action == NotifyCollectionChangedAction.Reset)
    //            Pins.Clear();

    //        if (e.OldItems != null)
    //        {
    //            foreach (CustomPin pin in e.OldItems)
    //            {
    //                Pins.Remove(pin);
    //                pin.PropertyChanged -= OnPropertyChanged;
    //            }
    //        }

    //        if (e.NewItems != null)
    //        {
    //            foreach (CustomPin pin in e.NewItems)
    //            {
    //                Pins.Add(pin);
    //                pin.PropertyChanged += OnPropertyChanged;
    //            }
    //        }
    //    }

    //    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        // We should be able to just replace the changed pin, but rebuild is required to force map refresh
    //        Pins.Clear();
    //        foreach (var pin in Items)
    //            Pins.Add(pin);
    //    }
    //}

    public class CustomPin : Pin
    {
        //public static readonly BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(CustomPin), default(Color));
        public static readonly BindableProperty OpacityProperty = BindableProperty.Create("Opacity", typeof(double), typeof(CustomPin), default(double));
        public static readonly BindableProperty PinNameProperty = BindableProperty.Create("PinName", typeof(string), typeof(CustomPin), default(string));


        //public Color Color
        //{
        //    get { return (Color)GetValue(ColorProperty); }
        //    set { SetValue(ColorProperty, value); }
        //}

        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        public string PinName
        {
            get { return (string)GetValue(PinNameProperty); }
            set { SetValue(PinNameProperty, value); }
        }
    }

    //public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    //{
    //    protected override MarkerOptions CreateMarker(Pin pin)
    //    {
    //        var cpin = pin as CustomPin;
    //        var hue = (float)cpin.Color.Hue % 1F * 360F;
    //        var alpha = (float)cpin.Opacity;

    //        var opts = new MarkerOptions();
    //        opts.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
    //        opts.SetTitle(pin.Label);
    //        opts.SetSnippet(pin.Address);
    //        opts.SetIcon(BitmapDescriptorFactory.DefaultMarker(hue));
    //        opts.SetAlpha(alpha);

    //        return opts;
    //    }
    //}
}
