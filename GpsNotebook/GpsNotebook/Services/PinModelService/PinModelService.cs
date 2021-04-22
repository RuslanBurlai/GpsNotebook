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

        public void AddPinLocation(PinModel pinLocation)
        {
            _repository.AddItem<PinModel>(pinLocation);
        }

        public IEnumerable<PinModel> GetAllPins()
        {
            return _repository.GetAllItems<PinModel>().Result.Where((id) => id.UserId == _authorization.GetUserId);
        }

        public void DeletePinLocation(PinModel pinLocation)
        {
            _repository.DeleteItem<PinModel>(pinLocation);
        }

        public IEnumerable<PinModel> SearchPins(string searchQuery)
        {
            return GetAllPins().Where((pin) =>
            pin.PinName.ToLower().Contains(searchQuery.ToLower()) ||
            pin.Description.ToLower().Contains(searchQuery.ToLower()));
        }
    }
}
