using ClassLibraryCafe;
using Microsoft.EntityFrameworkCore;

namespace CafeWeb.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
