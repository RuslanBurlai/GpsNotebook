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

        public void AddUser<User>(User user)
        {
            _repository.AddItem<User>(user);
        }

        public List<User> GetAllUser<User>()
        {
        }
    }
}
