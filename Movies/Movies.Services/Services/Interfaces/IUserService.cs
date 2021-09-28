using System.Collections.Generic;
using Movies.Services.ModelsDto;

namespace Movies.Services.Interfaces
{
    public interface IUserService
    {
        bool AddUser(UserAddDto userDto);
        UserDto GetUserById(int id);
        List<UserDto> GetAllUsers();
        List<MovieDto> GetRentedMovies(int userId);
        List<MovieDto> GetRentedMoviesByGenre(int? userId, int? genre);
    }
}
