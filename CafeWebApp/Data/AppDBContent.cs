using ClassLibraryCafe;
using Microsoft.EntityFrameworkCore;

namespace CafeWebApp.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }

        public DbSet<Product> product { get; set; }
        public DbSet<Combo> combo { get; set; }
    }
}
