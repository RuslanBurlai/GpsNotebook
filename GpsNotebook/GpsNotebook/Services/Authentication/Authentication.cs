using GpsNotebook.Model;
using System.Linq;
using GpsNotebook.Services.UserRepository;

namespace GpsNotebook.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        private IUserRepository _userRepository;
        public Authentication(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public bool IsRegisteredUser(User user)
        {
            User registeredUser = _userRepository.GetAllUser()
                .FirstOrDefault((x) => x.Email == user.Email && x.Password == user.Password);

            return registeredUser != null;
        }
    }
}
