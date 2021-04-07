using GpsNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetAllUser();
    }
}
