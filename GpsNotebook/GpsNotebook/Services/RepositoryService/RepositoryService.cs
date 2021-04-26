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

        public async Task<int> AddItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            if (item.Id != 0)
            {
                await _dataBase.Value.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                await _dataBase.Value.InsertAsync(item);
                return item.Id;
            }
        }

        public async Task<int> DeleteItem<T>(T item) where T : IEntityBaseForModel, new()
        {
            await _dataBase.Value.DeleteAsync(item);
            return item.Id;
        }

        public Task<List<T>> GetAllItems<T>() where T : IEntityBaseForModel, new()
        {
            return _dataBase.Value.Table<T>().ToListAsync();
        }
    }
}
