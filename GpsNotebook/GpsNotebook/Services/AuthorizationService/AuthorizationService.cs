using GpsNotebook.Services.SettingsManager;

namespace GpsNotebook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private ISettingsManager _settingManager;

        public AuthorizationService(
            ISettingsManager settingManager)
        {
            _settingManager = settingManager;
        }
        public int GetUserId
        {
            get { return _settingManager.UserId; }
        }

        public bool IsAuthorized
        {
            get { return _settingManager.UserId != 0; }
        }

        public int LogOut()
        {
            return _settingManager.UserId = 0;
        }
    }
}
