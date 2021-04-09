using GpsNotebook.Model;
using System.Linq;
using GpsNotebook.Services.UserRepository;
using GpsNotebook.Services.SettingsManager;

namespace GpsNotebook.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        private IUserRepository _userRepository;
        private ISettingsManager _settingsManager;

        public Authentication(
            IUserRepository repository,
            ISettingsManager settingsManager)
        {
            _userRepository = repository;
            _settingsManager = settingsManager;
        }
        public bool IsRegisteredUser(User user)
        {
            User registeredUser = _userRepository.GetAllUser()
                .FirstOrDefault((x) => x.Email == user.Email && x.Password == user.Password);

            if(registeredUser != null)
            {
                _settingsManager.Id = registeredUser.Id;
            }

            return registeredUser != null;
        }
    }
}
