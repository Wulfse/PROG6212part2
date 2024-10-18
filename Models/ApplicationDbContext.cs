using Microsoft.EntityFrameworkCore;
using PROG6212.Data;
using PROG6212.Models;

namespace PROG6212.Data  // Adjust this to your actual namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
       
    }
}
