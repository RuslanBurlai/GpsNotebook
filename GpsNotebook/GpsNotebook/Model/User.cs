using GpsNotebook.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Model
{
    [Table("AllUsers")]
    public class User : IEntityBaseForModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
