using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class UserDTO : GenericDTO
    {
        public new string Title { get => Email; set => Email = value; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}