using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using GoodsStore.Client.ViewModels.Abstract;
using GoodsStore.Client.ViewModels.Concrete;
using Microsoft.AspNetCore.Components;
using GoodsStore.Business.Models.Concrete;

namespace GoodsStore.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<IGenericCollectionVM<CategoryDTO>, GenericCollectionVM<CategoryDTO>>("BaseHttpClient",
                client => client.BaseAddress = new Uri($"https://localhost:44334/api/category"));

            builder.Services.AddHttpClient<IGenericItemVM<CategoryDTO>, GenericItem<CategoryDTO>>("BaseHttpClient",
                client =>
                {
                    client.BaseAddress = new Uri($"https://localhost:44334/api/category");
                });

            await builder.Build().RunAsync();
        }
    }
}
