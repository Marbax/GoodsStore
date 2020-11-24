using GoodsStore.Client.ViewModels.Abstract;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class GenericItem<T> : IGenericItemVM<T> where T : class
    {
        private readonly HttpClient _client;

        public T Item { get; set; }

        public bool IsReady
        {
            get
            {
                if (Item != null)
                    return true;
                return false;
            }
        }

        public string Message { get; set; }

        public GenericItem(HttpClient client)
        {
            _client = client;
        }

        public async Task GetItemAsync(int id)
        {
            try
            {
                Item = await _client.GetFromJsonAsync<T>($"{_client.BaseAddress}/{id}");
                Message = $"{Item} loaded successfully.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public async Task UpdateItem()
        {
            try
            {
                var res = await _client.PutAsJsonAsync<T>($"{_client.BaseAddress}", Item);
                var updated = await res.Content.ReadFromJsonAsync<T>();
                Item = updated;
                Message = $"{Item} successfully updated";
            }
            catch (Exception ex)
            {
                Message = $"{ex.Message} \nUpdating wasn't successfull.";
            }
        }

        public async Task AddItem()
        {
            try
            {
                var res = await _client.PostAsJsonAsync<T>($"{_client.BaseAddress}", Item);
                var added = await res.Content.ReadFromJsonAsync<T>();
                Item = added;
                Message = $"{Item} successfully added";
            }
            catch (Exception ex)
            {
                Message = $"{ex.Message} \nAdding wasn't successfull.";
            }
        }

    }
}
