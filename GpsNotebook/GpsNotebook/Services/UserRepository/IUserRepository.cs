using GpsNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.UserRepository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetAllUser();
    }
}
