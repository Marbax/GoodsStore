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
using GoodsStore.Client.Shared;
using GoodsStore.Client.Services.Abstract;
using GoodsStore.Client.Services.Concrete;

namespace GoodsStore.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IStorageService, LocalStorageService>();

            //FIXME: AddScoped
            builder.Services.AddScoped(x =>
            {
                var apiUrl = new Uri(builder.Configuration["apiUrl"]);
                return new HttpClient() { BaseAddress = apiUrl };
            });

            //builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44334/api/") });

            builder.Services.AddScoped<UserVM>();

            builder.Services.AddHttpClient<IGenericCollectionVM<CategoryDTO>, GenericCollectionVM<CategoryDTO>>("BaseHttpCatColClient",
                client => client.BaseAddress = new Uri($"https://localhost:44334/api/category"));

            builder.Services.AddHttpClient<IGenericItemVM<CategoryDTO>, GenericItem<CategoryDTO>>("BaseHttpCatClient",
                client => client.BaseAddress = new Uri($"https://localhost:44334/api/category"));

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();
        }
    }
}
