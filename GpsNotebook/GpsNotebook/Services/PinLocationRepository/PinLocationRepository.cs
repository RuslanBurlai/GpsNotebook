using GpsNotebook.Model;
using GpsNotebook.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.PinLocationRepository
{
    public class PinLocationRepository : IPinLocationRepository
    {
        private IRepository _repository;
        public PinLocationRepository(IRepository repository)
        {
            _repository = repository;
        }
        public void AddPinLocation(PinLocation pinLocation)
        {
            _repository.AddItem<PinLocation>(pinLocation);
        }

        public IEnumerable<PinLocation> GetPinsLocation()
        {
            return _repository.GetAllItems<PinLocation>();
        }

        public void DeletePinLocation(PinLocation pinLocation)
        {
            _repository.DeleteItem<PinLocation>(pinLocation);
        }
    }
}
