using GoodsStore.Business.Models.Concrete;
using GoodsStore.Client.Services.Abstract;
using GoodsStore.Client.Services.Concrete;
using GoodsStore.Client.ViewModels.Abstract;
using GoodsStore.Client.ViewModels.Concrete;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodsStore.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<AppSettings>(item => new AppSettings(builder.Configuration));

            builder.Services.AddScoped<HttpClient>();

            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IStorageService, LocalStorageService>();


            builder.Services.AddScoped<UserVM>();

            builder.Services.AddScoped<IGenericCollectionVM<CategoryDTO>, GenericCollectionVM<CategoryDTO>>();

            builder.Services.AddScoped<IGenericItemVM<CategoryDTO>, GenericItem<CategoryDTO>>();

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();
        }
    }
}
