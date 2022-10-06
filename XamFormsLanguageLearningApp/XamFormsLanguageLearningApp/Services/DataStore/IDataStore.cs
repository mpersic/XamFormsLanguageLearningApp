using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamFormsLanguageLearningApp.Services.DataStore
{
    public interface IDataStore<T>
    {
        #region Methods

        Task<bool> AddItemAsync(T item);

        Task<bool> DeleteItemAsync(string id);

        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<bool> UpdateItemAsync(T item);

        #endregion Methods
    }
}