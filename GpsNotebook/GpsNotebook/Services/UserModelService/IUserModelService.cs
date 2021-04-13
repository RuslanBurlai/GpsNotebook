using GpsNotebook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.UserModelService
{
    public interface IUserModelService
    {
        void AddUser(UserModel user);
        IEnumerable<UserModel> GetAllUser();
    }
}
