using GoodsStore.Business.Models.Concrete;
using System.Threading.Tasks;

namespace GoodsStore.Client.Services.Abstract
{
    public interface IAuthenticationService
    {
        UserDTO User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
        Task Register(string username, string password);
        Task UpdateProfile(UserDTO newData);
    }
}
