using GpsNotebook.Models;
using GpsNotebook.Services.RepositoryService;
using GpsNotebook.Services.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GpsNotebook.Services.PinLocationRepository
{
    public class PinModelService : IPinModelService
    {
        private IRepositoryService _repository;
        private IAuthorizationService _authorization;
        public PinModelService(
            IRepositoryService repository,
            IAuthorizationService authorization)
        {
            _repository = repository;
            _authorization = authorization;
        }

        public void AddPin(PinModel pinLocation)
        {
            _repository.AddItemAsync<PinModel>(pinLocation);
        }

        public IEnumerable<PinModel> GetAllPins()
        {
            return _repository.GetAllItemsAsync<PinModel>().Result.Where((id) => id.UserId == _authorization.GetUserId);
        }

        public void DeletePin(PinModel pinLocation)
        {
            _repository.DeleteItemAsync<PinModel>(pinLocation);
        }

        public IEnumerable<PinModel> SearchPins(string searchQuery)
        {
            return GetAllPins().Where((pin) =>
            pin.PinName.ToLower().Contains(searchQuery.ToLower()) ||
            pin.Description.ToLower().Contains(searchQuery.ToLower()));
        }

        public IEnumerable<PinModel> SearchByCategory(string category)
        {
            return GetAllPins().Where((pin) => pin.Categories == category);
        }
    }
}
