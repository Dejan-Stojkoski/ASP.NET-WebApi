using System.Collections.Generic;
using Movies.Services.ModelsDto;

namespace Movies.Services.Interfaces
{
    public interface IMovieService
    {
        bool AddMovie(MovieDto movieDto);
        MovieDto GetMovieById(int id);
        List<MovieDto> GetAllMovies();
        List<MovieDto> GetMovieByGenre(int genre);
    }
}
