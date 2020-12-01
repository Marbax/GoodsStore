using GoodsStore.Business.Models.Concrete;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class UserVM
    {
        private readonly HttpClient _client;

        public UserDTO User { get; set; } = new UserDTO();
        public UserDTO ProxyUser { get; set; } = new UserDTO();
        public string PasswordConfirm { get; set; } = "";

        public bool IsReady { get; private set; } = true;
        public bool IsSignedIn { get; set; } = false;
        public bool IsLoginDataValid { get => !string.IsNullOrEmpty(User.Email) && !string.IsNullOrEmpty(User.Password); }
        public string Message { get; set; } = "";

        public UserVM(HttpClient client)
        {
            _client = client;
        }


        public async Task<bool> Register()
        {
            if (ProxyUser.Password != PasswordConfirm)
            {
                Message = "Passwords aren't match.";
                return false;
            }

            try
            {
                var res = await _client.PostAsJsonAsync($"{_client.BaseAddress}/user/register", ProxyUser);
                if (!res.IsSuccessStatusCode)
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    Message = msg;
                    return false;
                }

                var added = await res.Content.ReadFromJsonAsync<UserDTO>();
                User = added;
                ProxyUser = new UserDTO();
                IsSignedIn = true;
                return true;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return false;
        }

        public async Task<bool> Login()
        {
            try
            {
                var res = await _client.PostAsJsonAsync($"{_client.BaseAddress}/user/login", ProxyUser);
                if (!res.IsSuccessStatusCode)
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    Message = msg;
                    return false;
                }

                var added = await res.Content.ReadFromJsonAsync<UserDTO>();
                User = added;
                ProxyUser = added.Clone();
                IsSignedIn = true;
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return false;
        }

        public async Task<bool> UpdateProfile()
        {
            if (ProxyUser.Password != User.Password)
                if (ProxyUser.Password != PasswordConfirm)
                {
                    Message = "Passwords aren't match.";
                    return false;
                }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_client.BaseAddress}/user/update");
                request.Content = new StringContent(JsonSerializer.Serialize(ProxyUser), Encoding.UTF8, "application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", User.Token);
                var res = await _client.SendAsync(request);

                //var res = await _client.PutAsJsonAsync($"{_client.BaseAddress}/user/update", ProxyUser);
                if (!res.IsSuccessStatusCode)
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    Message = msg;
                    return false;
                }

                var updated = await res.Content.ReadFromJsonAsync<UserDTO>();
                User = updated;
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return false;
        }

        public void Logout()
        {
            User = new UserDTO();
            IsSignedIn = false;
        }
    }
}
