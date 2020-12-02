using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Abstract
{
    public interface IGenericCollectionVM<T> where T : class
    {
        IEnumerable<T> Items { get; set; }
        bool IsReady { get; }
        string Message { get; set; }
        event Action OnChange;
        Task GetItemsAsync();
        Task RemoveItemAsync(int id);
    }
}
