using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Business.Models.Concrete
{

    public class UserDTO : GenericDTO
    {
        public new string Title
        {
            get => string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Lastname) ? Email : $"{ Lastname} { Name}";
        }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(60)]
        public string Password { get; set; }

        [StringLength(40)]
        public string Name { get; set; } = "";

        [StringLength(40)]
        public string Lastname { get; set; } = "";

        [StringLength(100)]
        public string Address { get; set; } = "";

        [StringLength(40)]
        public string Country { get; set; } = "";

        [StringLength(40)]
        public string Phone { get; set; } = "";

        public string Token { get; set; } = "";

        public IEnumerable<int> RoleIds { get; set; } = new List<int>();

        public override string ToString()
        {
            return Title;
        }

        public UserDTO Clone()
        {
            UserDTO other = (UserDTO)this.MemberwiseClone();

            return other;
        }
    }
}