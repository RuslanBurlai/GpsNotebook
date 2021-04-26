using GpsNotebook.Models;
using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserModelService;
using System.Linq;

namespace GpsNotebook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private ISettingsManager _settingsManager;
        private IUserModelService _userModelService;

        public AuthorizationService(
            ISettingsManager settingManager,
            IUserModelService userModelService)
        {
            _settingsManager = settingManager;
            _userModelService = userModelService;
        }

        public int GetUserId
        {
            get { return _settingsManager.UserId; }
        }

        public bool IsAuthorized
        {
            get { return _settingsManager.UserId != 0; }
        }

        public int LogOut()
        {
            return _settingsManager.UserId = 0;
        }

        public bool CheckRegistrationForUser(UserModel user)
        {
            UserModel registeredUser = _userModelService.GetAllUser()
                .FirstOrDefault((x) => x.Email == user.Email && x.Password == user.Password);

            if (registeredUser != null)
            {
                _settingsManager.UserId = registeredUser.Id;
            }

            return registeredUser != null;
        }
    }
}
