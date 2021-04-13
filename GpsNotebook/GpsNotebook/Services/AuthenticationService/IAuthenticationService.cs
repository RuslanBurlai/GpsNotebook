using GpsNotebook.Models;

namespace GpsNotebook.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsRegisteredUser(UserModel user);
    }
}
