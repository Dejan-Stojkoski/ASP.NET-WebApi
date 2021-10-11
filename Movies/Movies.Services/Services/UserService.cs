using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain.Models;
using Movies.Services.Helpers;
using Movies.Services.Interfaces;
using Movies.Services.Mapper;
using Movies.Services.ModelsDto;

namespace Movies.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IOptions<AppSettings> _options;

        public UserService(IRepository<User> userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }

        public UserDto Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var passwordData = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(passwordData);

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Username == username && x.Password == hashedPassword);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, $"{user.FullName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                FavouriteGenre = user.FavouriteGenre,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public bool Register(RegisterUserDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword) return false;
            if (string.IsNullOrEmpty(userDto.FullName)) return false;
            if (string.IsNullOrEmpty(userDto.Username)) return false;
            if (string.IsNullOrEmpty(userDto.Password)) return false;
            if (string.IsNullOrEmpty(userDto.ConfirmPassword)) return false;
            if (userDto.FavouriteGenre == null) return false;

            var md5 = new MD5CryptoServiceProvider();
            byte[] passwordData = md5.ComputeHash(Encoding.ASCII.GetBytes(userDto.Password));
            userDto.Password = Encoding.ASCII.GetString(passwordData);

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

        public bool DeleteUser(int id)
        {
            if(_userRepository.GetAll().Any(x => x.Id == id))
            {
                _userRepository.Delete(id);
                return true;
            }

            return false;
        }
    }
}
