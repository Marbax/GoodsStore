using GoodsStore.Client.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class GenericCollectionVM<T> : IGenericCollectionVM<T> where T : class
    {
        private readonly HttpClient _client;

        public IEnumerable<T> Items { get; set; }

        public bool IsReady
        {
            get
            {
                if (Items?.Count() > 0)
                    return true;
                return false;
            }
        }

        public string Message { get; set; }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public GenericCollectionVM(HttpClient client)
        {
            _client = client;
        }

        public async Task GetItemsAsync()
        {
            try
            {
                Items = await _client.GetFromJsonAsync<T[]>(_client.BaseAddress);
                NotifyStateChanged();
                Message = "Items loaded successfully.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public async Task RemoveItemAsync(int id)
        {
            try
            {
                var res = await _client.DeleteAsync($"{_client.BaseAddress}/{id}");
                var removed = await res.Content.ReadFromJsonAsync<T>();
                var items = Items.ToList();
                items.Remove(removed);
                Items = items;
                NotifyStateChanged();
                Message = $"{removed} successfulle removed.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }

}
