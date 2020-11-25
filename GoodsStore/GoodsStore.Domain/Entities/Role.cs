using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
