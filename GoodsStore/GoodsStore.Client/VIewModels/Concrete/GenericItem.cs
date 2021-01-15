using GoodsStore.Client.Services.Abstract;
using GoodsStore.Client.ViewModels.Abstract;
using System;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class GenericItem<T> : IGenericItemVM<T> where T : class
    {
        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;

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

        public GenericItem(IHttpService httpService, AppSettings appSettings)
        {
            _httpService = httpService;
            _appSettings = appSettings;
        }

        public async Task GetItemAsync(int id)
        {
            try
            {
                Item = await _httpService.Get<T>($"{_appSettings.GetController(typeof(T).Name)}/{id}");
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
                //FIXME: some bug with collection view , item remember different states and changes them randomly
                var res = await _httpService.Put<T>($"{_appSettings.GetController(typeof(T).Name)}", Item);
                Item = res;
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
                var res = await _httpService.Post<T>($"{_appSettings.GetController(typeof(T).Name)}", Item);
                Item = res;
                Message = $"{Item} successfully added";
            }
            catch (Exception ex)
            {
                Message = $"{ex.Message} \nAdding wasn't successfull.";
            }
        }

    }
}
