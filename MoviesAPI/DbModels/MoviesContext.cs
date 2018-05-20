using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.DbModels
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        { }

        public MoviesContext()
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<MovieRole> MovieRoles { get; set; }

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
