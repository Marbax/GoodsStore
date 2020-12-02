using System.Threading.Tasks;

namespace GoodsStore.Client.Services.Abstract
{
    public interface IHttpService
    {
        Task<T> Delete<T>(string uri, object value);
        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
        Task<T> Put<T>(string uri, object value);
    }
}
