using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Repository
{
    public interface IRepository
    {
        IEnumerable<T> GetAllItems<T>() where T : IEntityBaseForModel, new();
        void AddItem<T>(T item) where T : IEntityBaseForModel, new();
        void DeleteItem<T>(T item) where T : IEntityBaseForModel, new();
    }
}
