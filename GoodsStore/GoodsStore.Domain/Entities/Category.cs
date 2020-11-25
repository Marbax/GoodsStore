using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(400)]
        [DefaultValue("")]
        public string Description { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
