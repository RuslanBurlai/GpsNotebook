using SQLite;

namespace GpsNotebook.Models
{
    //rename to PinModel
    [Table("Pins")]
    public class PinModel : IEntityBaseForModel
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
