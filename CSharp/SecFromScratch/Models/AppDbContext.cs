using Microsoft.EntityFrameworkCore;
using SecFromScratch.Models;

namespace SecFromScratch {

    public class AppDbContext : DbContext {

        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
    }
}