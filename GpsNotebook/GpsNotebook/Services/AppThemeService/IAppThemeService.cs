using Xamarin.Forms.GoogleMaps;

namespace GpsNotebook.Services.AppThemeService
{
    public interface IAppThemeService
    {
        bool IsDarkTheme { get; }
        void SetUIAppTheme(string theme);
        MapStyle SetMapTheme(string mapTheme);
    }
}
