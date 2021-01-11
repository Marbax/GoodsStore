using GoodsStore.Business.Models.Concrete;
using GoodsStore.Client.Services.Abstract;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GoodsStore.Client.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly IStorageService _storageService;
        private readonly AppSettings _appSettings;

        public UserDTO User { get; private set; }

        public AuthenticationService(IHttpService httpService, NavigationManager navigationManager, IStorageService storageService, AppSettings appSettings)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _storageService = storageService;
            _appSettings = appSettings;
        }

        public async Task Initialize()
        {
            User = await _storageService.GetItem<UserDTO>("user");
        }

        public async Task Register(string username, string password)
        {
            User = await _httpService.Post<UserDTO>(_appSettings.RegisterUri.ToString(), new { username, password });
            await _storageService.SetItem("user", User);
        }

        public async Task UpdateProfile(UserDTO newData)
        {
            User = await _httpService.Put<UserDTO>(_appSettings.UpdateUri.ToString(), newData);
            await _storageService.SetItem("user", User);
        }

        public async Task Login(string username, string password)
        {
            User = await _httpService.Post<UserDTO>(_appSettings.LoginUri.ToString(), new { username, password });
            await _storageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _storageService.RemoveItem("user");
            _navigationManager.NavigateTo("/");
        }
    }
}
