using GpsNotebook.Models;
using GpsNotebook.Services.RepositoryService;
using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserModelService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpsNotebook.Services.UserRepository
{
    public class UserModelService : IUserModelService
    {
        private IRepositoryService _repository;
        private ISettingsManager _settingsManager;
        public UserModelService(
            IRepositoryService repository,
            ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        public void AddUser(UserModel user)
        {
            _repository.AddItemAsync(user);
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            return _repository.GetAllItemsAsync<UserModel>().Result;
        }

        public int GetUserId(string email, string password)
        {
            var userId = 0;
            var user = _repository.GetAllItemsAsync<UserModel>().Result
                .FirstOrDefault((x) => x.Email == email && x.Password == password);
            if (user != null)
            {
                _settingsManager.UserId = user.Id;
                return user.Id;
            }
            else
            {
                return userId;
            }
        }

    }
}
