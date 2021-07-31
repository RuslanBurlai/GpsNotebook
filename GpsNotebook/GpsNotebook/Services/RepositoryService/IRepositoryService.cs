using GpsNotebook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//to the folder Services
namespace GpsNotebook.Services.RepositoryService
{
    public interface IRepositoryService
    {
        Task<List<T>> GetAllItemsAsync<T>() where T : IEntityBaseForModel, new();
        Task<int> AddItemAsync<T>(T item) where T : IEntityBaseForModel, new();
        Task<int> DeleteItemAsync<T>(T item) where T : IEntityBaseForModel, new();
    }
}
