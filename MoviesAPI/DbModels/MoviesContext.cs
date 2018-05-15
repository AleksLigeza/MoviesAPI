using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.DbModels
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieRole> MovieRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Title = "Doddany przez Seed",
                Year = 2018,   
                Id = 10
            });
        }
    }
}
