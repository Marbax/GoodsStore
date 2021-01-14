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
        public Uri userController { get; set; }

        public AppSettings(WebAssemblyHostConfiguration config)
        {
            BaseUri = new Uri(config["apiUri"]);
            LoginUri = new Uri(BaseUri + config["loginMethod"]);
            RegisterUri = new Uri(BaseUri + config["registerMethod"]);
            UpdateUri = new Uri(BaseUri + config["updateProfileMethod"]);
            CategoriesController = new Uri(BaseUri + config["categoriesController"]);
            userController = new Uri(BaseUri + config["usersController"]);
        }

        public Uri GetController(string dtoName)
        {
            if (dtoName == typeof(CategoryDTO).Name)
                return CategoriesController;
            else if (dtoName == typeof(UserDTO).Name)
                return userController;
            else
                throw new ApplicationException($"Ther are no controller for {dtoName}.");
        }

    }
}
