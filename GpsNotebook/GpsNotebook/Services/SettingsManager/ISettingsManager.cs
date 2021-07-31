namespace GpsNotebook.Services.SettingsManager
{
    public interface ISettingsManager
    {
        int UserId { get; set; }
        string ApplicationTheme { get; set; }
    }
}
