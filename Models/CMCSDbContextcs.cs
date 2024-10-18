using Microsoft.EntityFrameworkCore;

namespace PROG6212.Models
{
    public class CMCSDbContext : DbContext
    {
        public CMCSDbContext(DbContextOptions<CMCSDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
    }
}