using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace GpsNotebook.Services.Permission
{
    public interface IPermissionService
    {
        Task<PermissionStatus> CheckPermissions(BasePermission permission);
    }
}
