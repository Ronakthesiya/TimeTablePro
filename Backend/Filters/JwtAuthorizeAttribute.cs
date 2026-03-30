using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace TimeTable_api
{
    // attribute for JWT authorization
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //return;

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing token");
                return;
            }

            string token = authHeader.Parameter;

            // You don't need to split or modify the token. Just use it directly for validation.
            byte[] key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWTKey"]);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate the token (entire token without modifying it)
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = ConfigurationManager.AppSettings["JWTIssuer"],  
                    ValidAudience = ConfigurationManager.AppSettings["JWTAudience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero  
                }, out SecurityToken validatedToken);

                // Attach user to request
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                    HttpContext.Current.User = principal;

                // Role check
                if (!string.IsNullOrEmpty(Roles))
                {
                    var roles = Roles.Split(',');
                    var hasRole = roles.Any(r => principal.IsInRole(r.Trim()));
                    if (!hasRole)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Forbidden: Role mismatch");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorize");
                return;
            }

            base.OnAuthorization(actionContext);
        }

        public static byte[] Base64UrlDecode(string input)
        {
            // Replace URL-safe characters with Base64 characters
            string base64 = input
                .Replace('-', '+')   // Replace '-' with '+'
                .Replace('_', '/');   // Replace '_' with '/'

            // Add padding if necessary (i.e., '=' characters)
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            // Decode the Base64 string
            return Convert.FromBase64String(base64);
        }
    }
}
