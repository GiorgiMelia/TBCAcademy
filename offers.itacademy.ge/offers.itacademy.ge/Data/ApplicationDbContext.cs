using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Web.Data;
using offers.itacademy.ge.Web.Models;

namespace offers.itacademy.ge.Web.Data
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
