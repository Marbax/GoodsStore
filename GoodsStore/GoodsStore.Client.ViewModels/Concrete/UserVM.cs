using GoodsStore.Business.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Client.ViewModels.Concrete
{
    public class UserVM
    {
        public UserDTO User { get; set; } = new UserDTO();
        public bool IsSignedIn { get; set; } = false;
        public string PasswordConfirm { get; set; } = "";
        public string Message { get; set; } = "";
        public async Task<bool> Register()
        {
            if (User.Password != PasswordConfirm)
            {
                Message = "Passwords aren't match.";
                return false;
            }

            await Task.Delay(1000);
            IsSignedIn = true;
            return true;
        }

        public async Task<bool> Login()
        {
            await Task.Delay(1000);
            IsSignedIn = true;
            return true;
        }

        public async Task Logout()
        {
            await Task.Delay(1000);
            IsSignedIn = false;
        }
    }
}
