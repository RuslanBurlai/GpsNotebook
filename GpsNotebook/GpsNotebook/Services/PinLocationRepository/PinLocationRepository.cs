using GpsNotebook.Model;
using GpsNotebook.Repository;
using GpsNotebook.Services.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace GpsNotebook.Services.PinLocationRepository
{
    public class PinLocationRepository : IPinLocationRepository
    {
        private IRepository _repository;
        private IAuthorization _authorization;
        public PinLocationRepository(
            IRepository repository,
            IAuthorization authorization)
        {
            _repository = repository;
            _authorization = authorization;
        }

        public void AddPinLocation(PinLocation pinLocation)
        {
            _repository.AddItem<PinLocation>(pinLocation);
        }

        public IEnumerable<PinLocation> GetPinsLocation()
        {
            return _repository.GetAllItems<PinLocation>().Where((id) => id.UserId == _authorization.GetUserId());
        }

        public void DeletePinLocation(PinLocation pinLocation)
        {
            _repository.DeleteItem<PinLocation>(pinLocation);
        }
    }
}
