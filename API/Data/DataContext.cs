using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, int,
     IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
     IdentityUserToken<int>>(options)
{
    public DbSet<AccountBalances> AccountBalances { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
             .HasMany(ur => ur.UserRoles)
             .WithOne(u => u.User)
             .HasForeignKey(ur => ur.UserId)
             .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<AccountBalances>(entity =>
        {
            entity.Property(t => t.RnD)
                .HasColumnType("decimal(18,2)");
            entity.Property(t => t.Canteen)
                .HasColumnType("decimal(18,2)");
            entity.Property(t => t.CeoCar)
                .HasColumnType("decimal(18,2)");
            entity.Property(t => t.Marketing)
                .HasColumnType("decimal(18,2)");
            entity.Property(t => t.ParkingFines)
                .HasColumnType("decimal(18,2)");
                
        });
    }
}
