using System;
using System.Text;
using API.Data;
using API.Entities;
using API.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt =>
         {
             opt.Password.RequireNonAlphanumeric = false;
         })
             .AddRoles<AppRole>()
             .AddRoleManager<RoleManager<AppRole>>()
             .AddEntityFrameworkStores<DataContext>();
             
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ??
                            throw new Exception("TokenKey Not found.");
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorizationBuilder()
             .AddPolicy(MemberPolicy.RequireAdminRole, policy => policy.RequireRole(MemberRole.Admin));

        return services;
    }
}
