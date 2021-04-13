using GpsNotebook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.PinLocationRepository
{ 
    //rename to IPinService
    public interface IPinModelService
    {
        void AddPinLocation(PinModel pinLocation);
        void DeletePinLocation(PinModel pinLocation);
        IEnumerable<PinModel> GetAllPins();
        IEnumerable<PinModel>SearchPins(string searchQuery);
    }
}
