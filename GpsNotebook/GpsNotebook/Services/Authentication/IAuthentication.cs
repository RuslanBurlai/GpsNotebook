using GpsNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.Authentication
{
    public interface IAuthentication
    {
        bool IsRegisteredUser(User user);
    }
}
