using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

[assembly: OwinStartup(typeof(GoodsStore.WebServer.App_Start.Startup))]

namespace GoodsStore.WebServer.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                            new JwtBearerAuthenticationOptions
                            {
                                AuthenticationMode = AuthenticationMode.Active,
                                TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ClockSkew = new TimeSpan(0),
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mPjVYomEjhmPjVYomEjhSDWhV7cT6K3UE6kq85GNQpSDWhV7cT6K3UE6mPjVYomEjhSDWhV7cT6K3UE6kq85GNQpkq85GNQp"))
                                }
                            });
        }
    }
}
