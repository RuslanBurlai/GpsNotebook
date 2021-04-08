using GpsNotebook.Model;
using GpsNotebook.Repository;
using System.Collections.Generic;

namespace GpsNotebook.Services.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private IRepository _repository;
        public UserRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void AddUser(User user)
        {
            _repository.AddItem(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            return _repository.GetAllItems<User>();
        }
    }
}
