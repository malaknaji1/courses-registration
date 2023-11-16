using System;
using System.Text;
using System.Threading.Tasks;
using courses_registration.Interfaces;
using courses_registration.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace courses_registration.Helpers
{
    public class BasicAuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        

        public BasicAuthFilter() 
        {
           
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
           
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic")) 
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var parts= credentials.Split(':');

                if(parts.Length == 2) 
                { 
                    var username= parts[0];
                    var reqHashedPassword=parts[1];
                    var dbHashedPassword =userRepository.GetPassword(username); 

                    if (userRepository.IsPasswordCorrect(reqHashedPassword,dbHashedPassword) )
                    {
                        return;
                    }
                }

            }
            context.Result = new UnauthorizedResult();
        }
        }
}
