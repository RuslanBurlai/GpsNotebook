using GpsNotebook.Models;
using GpsNotebook.Services.RepositoryService;
using GpsNotebook.Services.UserModelService;
using System.Collections.Generic;

namespace GpsNotebook.Services.UserRepository
{
    public class UserModelService : IUserModelService
    {
        private IRepositoryService _repository;
        public UserModelService(IRepositoryService repository)
        {
            _repository = repository;
        }

        public void AddUser(UserModel user)
        {
            _repository.AddItem(user);
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            return _repository.GetAllItems<UserModel>();
        }
    }
}
