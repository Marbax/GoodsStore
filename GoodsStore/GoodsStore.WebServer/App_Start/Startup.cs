using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(GoodsStore.WebServer.App_Start.Startup))]

namespace GoodsStore.WebServer.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
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
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Secret"]))
                                }
                            });
        }
    }
}
