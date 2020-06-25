using CodePepper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodePepper.DataAccess
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
