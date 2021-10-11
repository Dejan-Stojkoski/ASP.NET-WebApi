using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositories.EntityRepositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly MoviesDbContext _db;
        public UserRepository(MoviesDbContext db)
        {
            _db = db;
        }

        public int Add(User entity)
        {
            _db.Users.Add(entity);
            return _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            return _db.Users.Include(x => x.Rents)
                            .ThenInclude(x => x.MovieRents)
                            .ThenInclude(x => x.Movie)
                            .ToList();
        }

        public void Update(User entity)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == entity.Id);
            if (user != null)
            {
                user.FullName = entity.FullName;
                user.Username = entity.Username;
                user.Password = entity.Password;
                user.FavouriteGenre = entity.FavouriteGenre;
                _db.SaveChanges();
            }
        }
    }
}
