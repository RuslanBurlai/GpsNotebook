using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserRepository;

namespace GpsNotebook.Services.Authorization
{
    public class Authorization : IAuthorization
    {
        private ISettingsManager _settingManager;

        public Authorization(
            ISettingsManager settingManager)
        {
            _settingManager = settingManager;
        }

        public int GetUserId()
        {
            return _settingManager.Id;
        }

        public bool IsAuthorized()
        {
            return _settingManager.Id != 0;
        }

        public int LogOut()
        {
            return _settingManager.Id = 0;
        }
    }
}
