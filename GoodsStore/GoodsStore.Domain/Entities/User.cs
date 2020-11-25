using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(60)]
        public string Password { get; set; }

        [StringLength(40)]
        [DefaultValue("")]
        public string Name { get; set; }

        [StringLength(40)]
        [DefaultValue("")]
        public string Lastname { get; set; }

        [StringLength(100)]
        [DefaultValue("")]
        public string Address { get; set; }

        [StringLength(40)]
        [DefaultValue("")]
        public string Country { get; set; }

        [StringLength(40)]
        [DefaultValue("")]
        public string Phone { get; set; }

        [StringLength(1000)]
        [DefaultValue("")]
        public string Token { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
