using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Models;

namespace offers.itacademy.ge.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
