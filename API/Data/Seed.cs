using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using API.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, 
        RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if (users == null) return;

        var roles = new List<AppRole>()
        {
            new() {Name = MemberRole.Member},
            new() {Name = MemberRole.Admin},
            new() {Name = MemberRole.Moderator},
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach (var user in users)
        {
            user.UserName = user.UserName!.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user,MemberRole.Member);
        }

        var admin = new AppUser
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin,"Pa$$w0rd");
        await userManager.AddToRoleAsync(admin,MemberRole.Admin);
    }
}
