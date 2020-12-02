using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Abstract
{
    public interface IGenericItemVM<T> where T : class
    {
        T Item { get; set; }

        bool IsReady { get; }
        string Message { get; set; }

        Task AddItem();
        Task GetItemAsync(int id);
        Task UpdateItem();
    }
}
