using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.DataAccess.Repositories.EntityRepositories;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain;
using Movies.Domain.Models;

namespace Movies.Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesDbContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<IRepository<Movie>, MovieRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Rent>, RentRepository>();

            return services;
        }
    }
}
