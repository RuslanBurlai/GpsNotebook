using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GpsNotebook.Services.PermissionService
{
    public class PermissionsService : IPermissionService
    {
		private readonly IPermissions _permissionPluggin;
        public PermissionsService(IPermissions permissionPluggin)
        {
			_permissionPluggin = permissionPluggin;
		}

		public async Task<PermissionStatus> GetPermissionStatus<T>() where T : BasePermission, new()
		{
			PermissionStatus status = PermissionStatus.Unknown;
			try
			{
				status = await _permissionPluggin.RequestPermissionAsync<T>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return await Task.FromResult(status);
		}

		public async Task<bool> CheckPermission<T>() where T : BasePermission, new()
		{
			return await GetPermissionStatus<T>() == PermissionStatus.Granted;
		}
    }
}
