using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(Persons.Api.Startup))]
namespace Persons.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var key = ConfigurationManager.AppSettings["Jwt:Key"];
            var issuer = ConfigurationManager.AppSettings["Jwt:Issuer"];
            var audience = ConfigurationManager.AppSettings["Jwt:Audience"];

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = signingKey
                }
            });

            var config = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
