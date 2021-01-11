using GoodsStore.Client.Services.Abstract;
using GoodsStore.Client.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class GenericCollectionVM<T> : IGenericCollectionVM<T> where T : class
    {
        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;

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

        public GenericCollectionVM(IHttpService httpService, AppSettings appSettings)
        {
            _httpService = httpService;
            _appSettings = appSettings;
        }

        public async Task GetItemsAsync()
        {
            try
            {
                Items = await _httpService.Get<IEnumerable<T>>(_appSettings.CategoriesController.ToString());
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
                var res = await _httpService.Delete<T>($"{_appSettings.CategoriesController}/{id}");
                var items = Items.ToList();
                items.Remove(res);
                Items = items;
                NotifyStateChanged();
                Message = $"{res} successfully removed.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }

}
