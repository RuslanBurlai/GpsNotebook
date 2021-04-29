using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Styles;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Services.AppThemeService
{
    public class AppThemeService : IAppThemeService
    {
        private ISettingsManager _settingwManager;
        public AppThemeService(ISettingsManager settingsManager)
        {
            _settingwManager = settingsManager;
        }

        public bool IsDarkTheme
        {
            get { return _settingwManager.ApplicationTheme == nameof(DarkTheme); } 
        }

        public MapStyle SetMapTheme(string mapTheme)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"GpsNotebook.Resources.MapStyles.{mapTheme}.json");
            string styleFile;
            using (var reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            return MapStyle.FromJson(styleFile);
        }

        public void SetUIAppTheme(string theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (theme == nameof(DarkTheme))
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }
            _settingwManager.ApplicationTheme = theme;
        }
    }
}
