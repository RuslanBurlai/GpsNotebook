using Xamarin.Essentials;

namespace GpsNotebook.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        public int Id 
        {
            get => Preferences.Get(nameof(Id), 0);
            set => Preferences.Set(nameof(Id), value);
        }
    }
}
