using GoodsStore.Business.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Business.Models.Concrete
{
    public class GenericDTO : IEqualableByUID<GenericDTO>
    {
        public int Id { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is GenericDTO objAsSame)) return false;
            else return Equals(objAsSame);
        }

        public override int GetHashCode() => Id;

        public bool Equals(GenericDTO other)
        {
            if (other == null) return false;
            return Id.Equals(other.Id);
        }

        public override string ToString() => Title;

    }

}
