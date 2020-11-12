using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsStore.Business.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "Title";
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }
    public class UserDTO
    {
    }

    public class PhotoDTO
    {
    }

    public class SaleDTO
    {
    }

    public class SalePosDTO
    {
    }

    public class GoodDTO
    {
    }

}
