using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Domain.Entities
{
    public class Photo
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(100)]
        [DefaultValue("image/jpeg")]
        public string MimeType { get; set; }

        [Required]
        public byte[] PhotoData { get; set; }

        public virtual ICollection<Good> Goods { get; set; }

    }
}
