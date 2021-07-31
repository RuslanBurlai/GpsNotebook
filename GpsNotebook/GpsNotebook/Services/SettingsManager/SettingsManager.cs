using GpsNotebook.Styles;
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

        public string ApplicationTheme
        {
            get { return Preferences.Get(nameof(ApplicationTheme), nameof(LightTheme)); }
            set { Preferences.Set(nameof(ApplicationTheme), value); }
        }

    }
}
