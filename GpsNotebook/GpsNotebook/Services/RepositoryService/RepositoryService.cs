using System;
using System.Collections.Generic;
using System.IO;
using GpsNotebook.Models;
using SQLite;

namespace GpsNotebook.Services.RepositoryService
{
    //rename to RepositoryService change to the Async repository
    public class RepositoryService : IRepositoryService
    {
        public RepositoryService()
        {
            _dataBase = new Lazy<SQLiteConnection>(() =>
            {
                var dataBaseName = "UserAndPinLocations.db";
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), dataBaseName);
                var dataBase = new SQLiteConnection(dataBasePath);

                dataBase.CreateTable<UserModel>();
                dataBase.CreateTable<PinModel>();

                return dataBase;
            });
        }

        private Lazy<SQLiteConnection> _dataBase;
        public void AddItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            if (item.Id != 0)
            {
                _dataBase.Value.Update(item);
            }
            else
            {
                _dataBase.Value.Insert(item);
            }
        }

        public void DeleteItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            _dataBase.Value.Delete(item);
        }

        public IEnumerable<T> GetAllItems<T>() where T : IEntityBaseForModel, new()
        {
            return _dataBase.Value.Table<T>();
        }
    }
}
