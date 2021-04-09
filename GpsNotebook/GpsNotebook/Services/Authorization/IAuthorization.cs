using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.Authorization
{
    public interface IAuthorization
    {
        bool IsAuthorized();
        int LogOut();
        int GetUserId();
    }
}
