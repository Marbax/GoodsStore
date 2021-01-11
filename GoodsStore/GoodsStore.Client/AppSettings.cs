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

        public AppSettings(WebAssemblyHostConfiguration config)
        {
            BaseUri = new Uri(config["apiUri"]);
            LoginUri = new Uri(BaseUri + config["loginMethod"]);
            RegisterUri = new Uri(BaseUri + config["registerMethod"]);
            UpdateUri = new Uri(BaseUri + config["updateProfileMethod"]);
            CategoriesController = new Uri(BaseUri + config["categoriesController"]);
        }

    }
}
