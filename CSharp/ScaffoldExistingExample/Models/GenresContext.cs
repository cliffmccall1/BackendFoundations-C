using Microsoft.EntityFrameworkCore;

namespace ScaffoldExistingExample.Models {
    public class GenresContext : DbContext {
        public GenresContext (DbContextOptions<GenresContext> options) : base (options) {

        }
        public DbSet<genres> Genres { get; set; }
    }
}