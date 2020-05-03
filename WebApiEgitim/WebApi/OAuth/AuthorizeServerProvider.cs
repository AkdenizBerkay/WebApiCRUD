using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApi.DataModel;

namespace WebApi.OAuth
{
    public class AuthorizeServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // CORS ayarlarını set ediyoruz.
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "http://localhost:60556" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (NorthwindEntities db = new NorthwindEntities())
            {
                
                Users user = db.Users.Where(k => k.Mail.Equals(context.UserName, StringComparison.OrdinalIgnoreCase) && k.Password == context.Password).FirstOrDefault();
                if (user == null)
                {
                    context.SetError("Oturum Hatası", "Kullanıcı Adı veya Şifre Hatalı");
                }
                else
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("UserName", context.UserName));
                    identity.AddClaim(new Claim("Name", user.Name));
                    identity.AddClaim(new Claim("Surname", user.Surname));
                    identity.AddClaim(new Claim("RegisterNo", user.RegisterNo.ToString()));
                    identity.AddClaim(new Claim("Phone", user.Phone));
                    identity.AddClaim(new Claim("Mail", user.Mail));
                    identity.AddClaim(new Claim("Department", user.Department));
                    identity.AddClaim(new Claim("Birthday", user.Birthday.ToString()));
                    identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
                    context.Validated(identity);
                }
            }
        }
    }
}