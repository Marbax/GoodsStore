using System.Threading.Tasks;

namespace GoodsStore.Client.Services.Abstract
{
    public interface IStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }
}
