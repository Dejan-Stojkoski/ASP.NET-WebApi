using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositories.EntityRepositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly MoviesDbContext _db;
        public MovieRepository(MoviesDbContext db)
        {
            _db = db;
        }

        public int Add(Movie entity)
        {
            _db.Movies.Add(entity);
            return _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = _db.Movies.SingleOrDefault(x => x.Id == id);
            if(movie != null)
            {
                _db.Movies.Remove(movie);
                _db.SaveChanges();
            }
        }

        public List<Movie> GetAll()
        {
            return _db.Movies.Include(x => x.MovieRents)
                             .ThenInclude(x => x.Rent)
                             .ToList();
        }

        public void Update(Movie entity)
        {
            var movie = _db.Movies.SingleOrDefault(x => x.Id == entity.Id);
            if (movie != null)
            {
                movie.Title = entity.Title;
                movie.Description = entity.Description;
                movie.Genre = entity.Genre;
                movie.Year = entity.Year;
                _db.SaveChanges();
            }
        }
    }
}
