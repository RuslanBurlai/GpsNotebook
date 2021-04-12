using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GpsNotebook.Services.Permission
{
    public interface IPermission
    {
        Task<PermissionStatus> GetRequestPermissionAsync();
    }
}
