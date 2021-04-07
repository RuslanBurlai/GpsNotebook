using GpsNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Repository
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
