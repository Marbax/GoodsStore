using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Business.Models.Concrete
{
    public class GenericDTO : IEquatable<GenericDTO>, IComparable, IComparable<GenericDTO>
    {
        public int Id { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public virtual string Title { get; set; } = "";

        public sealed override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is GenericDTO objAsSame)) return false;
            else return Equals(objAsSame);
        }

        public bool Equals(GenericDTO other) => this == other;

        public sealed override int GetHashCode() => Id;

        public override string ToString() => Title;

        public int CompareTo(object obj) => (obj is GenericDTO dto) ? CompareTo(dto) : throw new ArgumentException($"Types aren't comparable - {GetType().Name} and { obj.GetType().Name}.", nameof(obj));

        public int CompareTo(GenericDTO other) => (GetType().Name == other.GetType().Name) ? Id.CompareTo(other.Id) : throw new ArgumentException($"Object Types aren't comparable - {GetType().Name} and { other.GetType().Name}.", nameof(other));

        public static bool operator ==(GenericDTO f, GenericDTO s)
        {
            if (f is null || s is null) return false;
            if (ReferenceEquals(f, s)) return true;
            if ((f.GetType().Name == s.GetType().Name)) return f.Id.Equals(s.Id);
            return false;
        }

        public static bool operator !=(GenericDTO f, GenericDTO s) => !(f == s);
    }
}
