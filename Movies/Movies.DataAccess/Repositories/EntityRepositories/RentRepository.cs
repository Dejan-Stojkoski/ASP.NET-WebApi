using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositories.EntityRepositories
{
    public class RentRepository : IRepository<Rent>
    {
        private readonly MoviesDbContext _db;
        public RentRepository(MoviesDbContext db)
        {
            _db = db;
        }

        public int Add(Rent entity)
        {
            _db.Rents.Add(entity);
            return _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rent = _db.Rents.SingleOrDefault(x => x.Id == id);
            if (rent != null)
            {
                _db.Rents.Remove(rent);
                _db.SaveChanges();
            }
        }

        public List<Rent> GetAll()
        {
            return _db.Rents.Include(x => x.MovieRents)
                            .ThenInclude(x => x.Movie)
                            .Include(x => x.User)
                            .ToList();
        }

        public void Update(Rent entity)
        {
            var rent = _db.Rents.SingleOrDefault(x => x.Id == entity.Id);
            if (rent != null)
            {
                rent.UserId = entity.UserId;
                _db.SaveChanges();
            }
        }
    }
}
