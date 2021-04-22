using GpsNotebook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//to the folder Services
namespace GpsNotebook.Services.RepositoryService
{
    public interface IRepositoryService
    {
        Task<List<T>> GetAllItems<T>() where T : IEntityBaseForModel, new();
        void AddItem<T>(T item) where T : IEntityBaseForModel, new();
        void DeleteItem<T>(T item) where T : IEntityBaseForModel, new();
    }
}
