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
            _dataBase = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var dataBaseName = "UserAndPinLocations.db";
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), dataBaseName);
                var dataBase = new SQLiteAsyncConnection(dataBasePath);

                dataBase.CreateTableAsync<UserModel>().Wait();
                dataBase.CreateTableAsync<PinModel>().Wait();

                return dataBase;
            });
        }

        private Lazy<SQLiteAsyncConnection> _dataBase;

        public void AddItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            if (item.Id != 0)
            {
                _dataBase.Value.UpdateAsync(item);
            }
            else
            {
                _dataBase.Value.InsertAsync(item);
            }
        }

        public void DeleteItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            _dataBase.Value.DeleteAsync(item);
        }

        public IEnumerable<T> GetAllItems<T>() where T : IEntityBaseForModel, new()
        {
            return (IEnumerable<T>)_dataBase.Value.Table<T>();
        }
    }
}
