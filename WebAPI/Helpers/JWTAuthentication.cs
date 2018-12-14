using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;

using WebApi.Helpers.Authorization;
using WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Linq;
using WebApi.Helpers.Exceptions;

namespace WebApi.Helpers
{
    public class JWTAuthentication
    {
        /// <summary>
        /// Configures jwt authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="secretKey"></param>
        public static void Configure(IServiceCollection services, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);

            //Add access policies and roles
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AgentsAndAbove, policy => policy.RequireRole(Role.GetSpecifiedAndHigherRoles(Role.RoleId.Agent).Select(x => x.ToString()).ToArray()));
                options.AddPolicy(Policies.AdminsAndAbove, policy => policy.RequireRole(Role.GetSpecifiedAndHigherRoles(Role.RoleId.Admin).Select(x => x.ToString()).ToArray()));
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if(!IsUserValid(context))
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static bool IsUserValid(TokenValidatedContext context)
        {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var userId = int.Parse(context.Principal.Identity.Name);

            try
            {
                var user = userService.GetById(userId);
                return true;
            }
            catch (NotFoundException)
            {
                //user no longer exists
                return false;
            }
        }

        public static string GetToken(UserDetailsModel user, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}