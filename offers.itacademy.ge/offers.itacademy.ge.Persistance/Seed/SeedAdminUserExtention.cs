using ITAcademy.Offers.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.Offers.Persistence.Seed
{
    public static class SeedAdminUserExtensions
    {
        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            var roleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            var adminId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = adminId,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = "AQAAAAIAAYagAAAAENX4Q7sbsVk3vLjpGJMfrnY2mvJCXcdnkiyHWWkSGCCAsX24I/rur8CAULHlolZoGw==",
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
