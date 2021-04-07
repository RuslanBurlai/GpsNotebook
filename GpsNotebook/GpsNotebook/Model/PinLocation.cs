using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Model
{
    public class PinLocation
    {
        public int Id { get; set; }
        public string PinName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int UserId { get; set; }
    }
}
