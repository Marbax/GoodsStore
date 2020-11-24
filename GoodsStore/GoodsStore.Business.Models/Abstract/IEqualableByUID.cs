using System;

namespace GoodsStore.Business.Models.Abstract
{
    public interface IEqualableByUID<T> : IEquatable<T>
    {
        int Id { get; set; }
        string Title { get; set; }
    }
}
