using GpsNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Services.PinLocationRepository
{ 
    //rename to IPinService
    public interface IPinLocationRepository
    {
        void AddPinLocation(PinLocation pinLocation);
        void DeletePinLocation(PinLocation pinLocation);
        IEnumerable<PinLocation> GetPinsLocation();
    }
}
