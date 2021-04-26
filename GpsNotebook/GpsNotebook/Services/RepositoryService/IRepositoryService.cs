using GpsNotebook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//to the folder Services
namespace GpsNotebook.Services.RepositoryService
{
    public interface IRepositoryService
    {
        Task<List<T>> GetAllItems<T>() where T : IEntityBaseForModel, new();
        Task<int> AddItem<T>(T item) where T : IEntityBaseForModel, new();
        Task<int> DeleteItem<T>(T item) where T : IEntityBaseForModel, new();
    }
}
