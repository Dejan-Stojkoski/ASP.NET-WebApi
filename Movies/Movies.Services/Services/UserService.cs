using System.Collections.Generic;
using System.Linq;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain.Models;
using Movies.Services.Interfaces;
using Movies.Services.Mapper;
using Movies.Services.ModelsDto;

namespace Movies.Web.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUser(UserAddDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FullName) ||
                string.IsNullOrEmpty(userDto.Username) ||
                string.IsNullOrEmpty(userDto.Password) ||
                userDto.Subscription == null)
            {
                return false;
            }

            var user = (User)GenericMapper.MapObject(userDto, new User());
            _userRepository.Add(user);
            return true;
        }

        public List<UserDto> GetAllUsers()
        {
            return _userRepository.GetAll()
                                  .Select(x => (UserDto)GenericMapper.MapObject(x, new UserDto()))
                                  .ToList();
        }

        public List<MovieDto> GetRentedMovies(int userId)
        {
            return _userRepository.GetAll()
                          .SingleOrDefault(x => x.Id == userId)?
                          .Rents?.SelectMany(x => x.MovieRents)
                          .Select(x => x.Movie)
                          .Select(x => (MovieDto)GenericMapper.MapObject(x, new MovieDto())).ToList();
        }

        public List<MovieDto> GetRentedMoviesByGenre(int? userId, int? genre)
        {
            return _userRepository.GetAll()
                          .SingleOrDefault(x => x.Id == userId)?
                          .Rents?.SelectMany(x => x.MovieRents)
                          .Select(x => x.Movie)
                          .Where(x => x.Genre == genre)
                          .Select(x => (MovieDto)GenericMapper.MapObject(x, new MovieDto())).ToList();
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetAll().SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                return (UserDto)GenericMapper.MapObject(user, new UserDto());
            }

            return null;
        }
    }
}
