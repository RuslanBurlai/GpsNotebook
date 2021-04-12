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

        //public int UserId
        //{
        //    get => _settingManager.Id;
        //}

        //add property for this
        public int GetUserId()
        {
            return _settingManager.Id;
        }

        //add property for this
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
