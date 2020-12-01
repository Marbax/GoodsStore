namespace GoodsStore.Domain.Context
{
    using GoodsStore.Domain.Entities;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class GoodsStoreDBInitializer : DropCreateDatabaseIfModelChanges<GoodsStoreDB>
    {
        protected override void Seed(GoodsStoreDB context)
        {
            IEnumerable<Manufacturer> mans = new List<Manufacturer>() {
                new Manufacturer(){Id = 1,Title= "Samsung"},
                new Manufacturer(){Id = 2,Title= "Lenovo"},
                new Manufacturer(){Id = 3,Title= "Nokia"},
                new Manufacturer(){Id = 4,Title= "Huawei"},
                new Manufacturer(){Id = 5,Title= "Sony"},
                new Manufacturer(){Id = 6,Title= "LG" , Description = "Nice monitors producer."},
                new Manufacturer(){Id = 7,Title= "Acer"},
                new Manufacturer(){Id = 8,Title= "HP" , Description="Choppiest notebooks producer."},
                new Manufacturer(){Id = 9,Title= "Canon" , Description = "Best Printer producer."},
                new Manufacturer() {Id = 10, Title = "Asus"}
            };
            context.Manufacturers.AddRange(mans);
            context.SaveChanges();

            IEnumerable<Category> cats = new List<Category>() {
                 new Category(){Id=1,Title= "Smartphone"},
                 new Category(){Id=2,Title= "Notebook" , Description = "You can bring it anywhere rather than system block with monitor."},
                 new Category(){Id=3,Title= "Printer" , Description = "With that you can print your documents and photos."},
                 new Category(){Id=4,Title= "Telephone",Description = "Allows you to call at any moment."}
            };
            context.Categories.AddRange(cats);
            context.SaveChanges();

            IEnumerable<Good> goods = new List<Good>() {
                new Good(){ Id=1, Title= "HP LaserJet P2035 (CE461A)",  ManufacturerId = 8, CategoryId=3,  Price = 5445, Count= 12},
                new Good(){ Id=2, Title= "Canon i-SENSYS MF212W with Wi-F" , ManufacturerId =9, CategoryId=3, Price=3999, Count =7 ,Description="Best equivalent of productivity and value."},
                new Good(){ Id=3, Title= "Samsung SCX-4650N ",  ManufacturerId = 1, CategoryId=3, Price = 3999, Count= 15},
                new Good(){ Id=4, Title= "HP DJ1510 (B2L56C) ",  ManufacturerId = 8, CategoryId=3, Price = 1199, Count= 2},
                new Good(){ Id=5, Title= "Asus Transformer Book T100TAF 32GB",  ManufacturerId =10, CategoryId=2,  Price = 4999, Count= 11},
                new Good(){ Id=6, Title= "Lenovo Flex 10 (59427902)",  ManufacturerId = 2, CategoryId=  2, Price= 5555, Count= 11},
                new Good(){ Id=7, Title= "Acer Aspire ES1-411-C1XZ",  ManufacturerId = 7, CategoryId=2, Price = 6399, Count= 7},
                new Good(){ Id=8, Title= "HP 255 G4 (N0Y69ES)",  ManufacturerId =8,  CategoryId= 2, Price= 6199, Count= 5},
                new Good(){ Id=9, Title= "HP ProBook 430 (N0Y70ES)",  ManufacturerId = 8, CategoryId=2, Price = 12400, Count= 3},
                new Good(){ Id=10, Title= "Ultrabook Samsung 535U3",  ManufacturerId = 1, CategoryId= 3, Price= 8000, Count=10 , Description = "Ultra thin , ultra modern."},
                new Good(){ Id=11, Title= "Samsung Galaxy S3 Neo Duos I9300i Black",  ManufacturerId = 1, CategoryId=1, Price = 3999, Count= 7 , Description = "Better than IPhone."},
                new Good(){ Id=12, Title= "Lenovo A5000 Black",  ManufacturerId = 2, CategoryId=1, Price = 3299, Count= 5},
                new Good(){ Id=13, Title= "Microsoft Lumia 640 XL (Nokia)",  ManufacturerId = 3, CategoryId=1,  Price = 4888, Count= 5 , Description="Do not be scaried thats nokia not an Microsoft gadget."},
                new Good(){ Id=14, Title= "LG G3s Dual D724 Titan",  ManufacturerId = 6, CategoryId= 1, Price = 3999, Count= 0}
            };
            context.Goods.AddRange(goods);
            context.SaveChanges();

            var adm = new Role() { Id = 1, Title = "superadmin" };
            IEnumerable<Role> roles = new List<Role>() {
                adm,
                 new Role(){Id=2,Title= "admin" },
                 new Role(){Id=3,Title= "manager" },
                 new Role(){Id=4,Title= "cashier" },
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            IEnumerable<User> users = new List<User>() { new User() { Id = 1, Email = "superadmin@gs.com", Password = "superadmin", Roles = roles.ToList() } };
            context.Users.AddRange(users);
            context.SaveChanges();



        }
    }

}
