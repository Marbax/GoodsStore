namespace GoodsStore.Business.Models.Concrete
{
    public class PhotoDTO : GenericDTO
    {
        public string MimeType { get; set; }
        public byte[] PhotoData { get; set; }

    }


}