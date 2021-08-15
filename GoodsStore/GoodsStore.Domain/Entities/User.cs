using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(40)]
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

        [Required]
        [StringLength(40)]
        [Index(IsUnique = true)]
        public string Phone { get; set; }

        [StringLength(1000)]
        [DefaultValue("")]
        public string Token { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }
}
