using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GpsNotebook.Services.Permission
{
    public interface IPermissionService
    {
        Task<PermissionStatus> GetRequestPermissionAsync();
    }
}
