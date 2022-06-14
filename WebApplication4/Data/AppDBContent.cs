using ClassLibraryCafe;
using Microsoft.EntityFrameworkCore;

namespace CafeWeb.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
