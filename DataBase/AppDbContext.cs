using AngularApi.DataBase.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.DataBase
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        public DbSet<Genera> generas { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Tv> tvs { get; set; }
       
    }
}
