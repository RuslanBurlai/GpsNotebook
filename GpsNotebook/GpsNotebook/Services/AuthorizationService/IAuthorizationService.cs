namespace GpsNotebook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        int GetUserId { get; }
        int LogOut();
    }
}
