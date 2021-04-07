using GpsNotebook.Repository;
using SQLite;

namespace GpsNotebook.Model
{
    [Table("Pins")]
    public class PinLocation : IEntityBaseForModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PinName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int UserId { get; set; }
    }
}
