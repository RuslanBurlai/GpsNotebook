using GpsNotebook.Models;
using System.Collections.Generic;

namespace GpsNotebook.Services.UserModelService
{
    public interface IUserModelService
    {
        void AddUser(UserModel user);
        IEnumerable<UserModel> GetAllUser();
    }
}
