using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.Services.Interfaces;
using Movies.Services.Mapper;
using Movies.Services.ModelsDto;
using Movies.ViewModels;
using Movies.ViewModels.Enums;

namespace Movies.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("allMovies")]
        public ActionResult GetAll()
        {
            var movies = _movieService.GetAllMovies();
            if (movies.Count == 0)
            {
                return NoContent();
            }
            
            return Ok(movies.Select(x => (MovieViewModel)GenericMapper.MapObject(x, new MovieViewModel())));
        }

        [HttpPost("addMovie")]
        public ActionResult AddNewMovie([FromBody] MovieAddViewModel movie)
        {
            if (_movieService.AddMovie((MovieDto)GenericMapper.MapObject(movie, new MovieDto())))
            {
                return Ok($"Movie {movie.Title} added successfully!");
            }

            return BadRequest("Invalid format or cotent missing!");
        }

        [HttpGet("movie/{id:int}")]
        public ActionResult GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if(movie != null)
            {
                return Ok(GenericMapper.MapObject(movie, new MovieViewModel()));
            }

            return BadRequest($"No movie with ID:{id}");
        }

        [HttpGet("moviesGenre/{genre}")]
        public IActionResult GetMoviesByGenre(MovieGenre genre)
        {
            var movies = _movieService.GetMovieByGenre((int)genre);
            if (movies.Count != 0)
            {
                return Ok(movies.Select(x => (MovieViewModel)GenericMapper.MapObject(x, new MovieViewModel())));
            }

            return NoContent();
        }
    }
}
