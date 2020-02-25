using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Domain.Common
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Get(string Id);
        Task Delete(string Id);
        Task Add(T obj);
        Task Update(T obj);
        string SortOrder { get; set; }
        string SearchString { get; set; }
        int PageIndex { get; set; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }

    }
}