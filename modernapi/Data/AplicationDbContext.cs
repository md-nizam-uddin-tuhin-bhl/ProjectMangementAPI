using Microsoft.EntityFrameworkCore;
using modernapi.Models;

namespace modernapi.Data
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions options):base(options) { }
        public DbSet<Product> products { get; set; }
    }
   
}
