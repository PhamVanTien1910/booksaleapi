using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task RemoveAsync(string key);
        Task<bool> ExistAsync(string key);
    }
}
