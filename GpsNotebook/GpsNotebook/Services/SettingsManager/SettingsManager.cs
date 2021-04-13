using Xamarin.Essentials;

namespace GpsNotebook.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        public int UserId 
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }
    }
}
