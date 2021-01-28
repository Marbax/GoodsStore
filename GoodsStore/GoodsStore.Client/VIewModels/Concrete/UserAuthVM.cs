using GoodsStore.Business.Models.Concrete;
using GoodsStore.Client.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class UserAuthVM
    {
        public IAuthenticationService AuthSrvice { get; }
        public UserDTO ProxyUser { get; set; } = new UserDTO();
        public string PasswordConfirm { get; set; } = "";

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
        public bool IsReady { get; private set; } = true;
        public bool IsLoginDataValid { get => !string.IsNullOrEmpty(ProxyUser.Email) && !string.IsNullOrEmpty(ProxyUser.Password); }
        public string Message { get; set; } = "";

        public UserAuthVM(IAuthenticationService authSrvice)
        {
            AuthSrvice = authSrvice;
            if (!(AuthSrvice.User is null))
                ProxyUser = AuthSrvice.User.Clone();
        }

        public bool IsSignedIn() => !(AuthSrvice.User is null);

        public async Task<bool> Register()
        {
            if (ProxyUser.Password != PasswordConfirm)
            {
                Message = "Passwords aren't match.";
                return false;
            }

            try
            {
                IsReady = false;
                Message = "";
                NotifyStateChanged();
                await AuthSrvice.Register(ProxyUser.Email, ProxyUser.Password);
                ProxyUser = AuthSrvice.User.Clone();
                IsReady = true;
                NotifyStateChanged();
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
                IsReady = false;
                Message = "";
                NotifyStateChanged();
                await AuthSrvice.Login(ProxyUser.Email, ProxyUser.Password);
                ProxyUser = AuthSrvice.User.Clone();
                IsReady = true;
                NotifyStateChanged();
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
            if (ProxyUser.Password != AuthSrvice.User.Password)
                if (ProxyUser.Password != PasswordConfirm)
                {
                    Message = "Passwords aren't match.";
                    return false;
                }

            try
            {
                IsReady = false;
                Message = "";
                NotifyStateChanged();
                await AuthSrvice.UpdateProfile(ProxyUser);
                ProxyUser = AuthSrvice.User.Clone();
                IsReady = true;
                NotifyStateChanged();
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
            AuthSrvice.Logout();
            ProxyUser = new UserDTO();
            NotifyStateChanged();
        }
    }
}
