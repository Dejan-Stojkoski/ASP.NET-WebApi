using System.Collections.Generic;
using Movies.Services.ModelsDto;

namespace Movies.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        bool Register(RegisterUserDto model);
        UserDto GetUserById(int id);
        List<UserDto> GetAllUsers();
        List<MovieDto> GetRentedMovies(int userId);
        List<MovieDto> GetRentedMoviesByGenre(int? userId, int? genre);
        bool DeleteUser(int id);
    }
}
