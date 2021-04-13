using GpsNotebook.Models;
using System.Linq;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Services.SettingsManager;

namespace GpsNotebook.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserModelService _userRepository;
        private ISettingsManager _settingsManager;

        public AuthenticationService(
            IUserModelService repository,
            ISettingsManager settingsManager)
        {
            _userRepository = repository;
            _settingsManager = settingsManager;
        }
        public bool IsRegisteredUser(UserModel user)
        {
            UserModel registeredUser = _userRepository.GetAllUser()
                .FirstOrDefault((x) => x.Email == user.Email && x.Password == user.Password);

            if (registeredUser != null)
            {
                _settingsManager.UserId = registeredUser.Id;
            }

            return registeredUser != null;
        }
    }
}
