using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.Domain
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options){}

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rent> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(x => x.MovieRents)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<Rent>()
                .HasMany(x => x.MovieRents)
                .WithOne(x => x.Rent)
                .HasForeignKey(x => x.RentId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Rents)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            //SEED
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        FullName = "Jon Jonsky",
                        Password = "jonjon",
                        Subscription = 1,
                        Username = "jonS",
                    },
                    new User
                    {
                        Id = 2,
                        FullName = "Jill Jillsky",
                        Password = "jilljill",
                        Subscription = 2,
                        Username = "jillJ"
                    },
                    new User
                    {
                        Id = 3,
                        FullName = "Greg Gregsky",
                        Password = "greggreg",
                        Subscription = 1,
                        Username = "gregG"
                    }
                );

            modelBuilder.Entity<Movie>()
                .HasData(
                    new Movie
                    {
                        Id = 1,
                        Title = "The Hangover",
                        Description = "This is a comedy movie.",
                        Genre = 1,
                        Year = 2009
                    },
                    new Movie
                    {
                        Id = 2,
                        Title = "Mortal Kombat",
                        Description = "This is an action movie.",
                        Genre = 2,
                        Year = 2021
                    },
                    new Movie
                    {
                        Id = 3,
                        Title = "Inception",
                        Description = "This is a Sci-fi movie.",
                        Genre = 3,
                        Year = 2010
                    },
                    new Movie
                    {
                        Id = 4,
                        Title = "Spy",
                        Description = "This is a comedy movie.",
                        Genre = 1,
                        Year = 2015
                    }
                );

            modelBuilder.Entity<Rent>()
                .HasData(
                    new Rent
                    {
                        Id = 1,
                        UserId = 1
                    },
                    new Rent
                    {
                        Id = 2,
                        UserId = 2
                    },
                    new Rent
                    {
                        Id = 3,
                        UserId = 1
                    }
                );

            modelBuilder.Entity<MovieRent>()
                .HasData(
                    new MovieRent
                    {
                        Id = 1,
                        MovieId = 2,
                        RentId = 1
                    },
                     new MovieRent
                     {
                         Id = 2,
                         MovieId = 3,
                         RentId = 1
                     },
                     new MovieRent
                     {
                         Id = 3,
                         MovieId = 1,
                         RentId = 2
                     },
                     new MovieRent
                     {
                         Id = 4,
                         MovieId = 3,
                         RentId = 2
                     },
                     new MovieRent
                     {
                         Id = 5,
                         MovieId = 1,
                         RentId = 3
                     }
                );
        }
    }
}
