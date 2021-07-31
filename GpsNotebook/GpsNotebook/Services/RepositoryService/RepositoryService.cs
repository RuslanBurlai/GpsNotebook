using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public async Task<int> AddItemAsync<T>(T item) where T : IEntityBaseForModel, new()
        {
            if (item.Id != 0)
            {
                return await _dataBase.Value.UpdateAsync(item);
                //return item.Id;
            }
            else
            {
                return await _dataBase.Value.InsertAsync(item);
                //return item.Id;
            }
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : IEntityBaseForModel, new()
        {
            return await _dataBase.Value.DeleteAsync(item);
        }

        public Task<List<T>> GetAllItemsAsync<T>() where T : IEntityBaseForModel, new()
        {
            return _dataBase.Value.Table<T>().ToListAsync();
        }
    }
}
