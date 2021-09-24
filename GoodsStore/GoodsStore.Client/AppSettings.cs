using GoodsStore.Business.Models.Concrete;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

namespace GoodsStore.Client
{
    public class AppSettings
    {
        public Uri BaseUri { get; set; }
        public Uri LoginUri { get; set; }
        public Uri RegisterUri { get; set; }
        public Uri UpdateUri { get; set; }
        public Uri CategoriesController { get; set; }
        public Uri UserController { get; set; }
        public Uri RoleController { get; set; }
        public Uri GoodController { get; set; }

        public AppSettings(WebAssemblyHostConfiguration config)
        {
            BaseUri = new Uri(config["apiUri"]);
            LoginUri = new Uri(BaseUri + config["loginMethod"]);
            RegisterUri = new Uri(BaseUri + config["registerMethod"]);
            UpdateUri = new Uri(BaseUri + config["updateProfileMethod"]);
            CategoriesController = new Uri(BaseUri + config["categoriesController"]);
            UserController = new Uri(BaseUri + config["usersController"]);
            RoleController = new Uri(BaseUri + config["rolesController"]);
            GoodController = new Uri(BaseUri + config["goodsController"]);
        }

        public Uri GetController(string dtoName)
        {
            if (dtoName == typeof(CategoryDTO).Name)
                return CategoriesController;
            else if (dtoName == typeof(UserDTO).Name)
                return UserController;
            else if (dtoName == typeof(RoleDTO).Name)
                return RoleController;
            else if (dtoName == typeof(GoodDTO).Name)
                return GoodController;
            else
                throw new ApplicationException($"Ther are no controller for {dtoName}.");
        }

    }
}
