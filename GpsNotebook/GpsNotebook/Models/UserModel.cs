using SQLite;

namespace GpsNotebook.Models
{
    [Table("AllUsers")]
    public class UserModel : IEntityBaseForModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
