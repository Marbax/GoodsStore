using GoodsStore.Business.Models.Concrete;
using GoodsStore.Client.Services.Abstract;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsStore.Client.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private IStorageService _storageService;

        public UserDTO User { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            IStorageService storageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _storageService = storageService;
        }

        public async Task Initialize()
        {
            User = await _storageService.GetItem<UserDTO>("user");
        }

        public async Task Register(string username, string password)
        {
            User = await _httpService.Post<UserDTO>("user/register", new { username, password });
            await _storageService.SetItem("user", User);
        }

        public async Task UpdateProfile(UserDTO newData)
        {
            User = await _httpService.Put<UserDTO>("user/update", newData);
            await _storageService.SetItem("user", User);
        }

        public async Task Login(string username, string password)
        {
            User = await _httpService.Post<UserDTO>("user/login", new { username, password });
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
