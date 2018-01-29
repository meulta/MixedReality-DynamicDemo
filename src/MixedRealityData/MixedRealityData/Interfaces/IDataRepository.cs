using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VirtualTourApi.Interfaces
{
    public interface IDataRepository<TModel>
    {
        Task InitializeDatabaseAsync(string databaseId, string collectionId);

        Task<TModel> FetchItemAsync(string id);

        Task<IEnumerable<TModel>> FetchItemsAsync();

        Task<IEnumerable<TModel>> FindItemsAsync(Expression<Func<TModel, bool>> predicate);

        Task<TModel> CreateItemAsync(TModel item);

        Task<TModel> CreateItemIfNotExistsAsync(TModel item);

        Task<TModel> UpdateItemAsync(string id, TModel item);

        Task DeleteItemAsync(string id);
    }
}