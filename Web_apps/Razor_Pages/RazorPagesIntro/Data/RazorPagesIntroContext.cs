using Microsoft.EntityFrameworkCore;

namespace RazorPagesIntro.Data
{
    public class RazorPagesIntroContext : DbContext
    {
        public RazorPagesIntroContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}