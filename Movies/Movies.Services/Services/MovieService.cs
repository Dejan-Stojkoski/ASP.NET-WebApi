using System.Collections.Generic;
using System.Linq;
using Movies.DataAccess.Repository.IRepository;
using Movies.Domain.Models;
using Movies.Services.Interfaces;
using Movies.Services.Mapper;
using Movies.Services.ModelsDto;

namespace Movies.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public bool AddMovie(MovieDto movieDto)
        {
            if (string.IsNullOrEmpty(movieDto.Title) || movieDto.Genre == null)
            {
                return false;
            }

            var movie = (Movie)GenericMapper.MapObject(movieDto, new Movie());
            _movieRepository.Add(movie);
            return true;
        }

        public List<MovieDto> GetAllMovies()
        {
            return _movieRepository.GetAll()
                                   .Select(x => (MovieDto)GenericMapper.MapObject(x, new MovieDto()))
                                   .ToList();
        }

        public List<MovieDto> GetMovieByGenre(int genre)
        {
            var genreDomain = genre;
            return _movieRepository.GetAll().Where(x => x.Genre == genre)?
                                .Select(x => (MovieDto)GenericMapper.MapObject(x, new MovieDto()))
                                .ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            var movie = _movieRepository.GetAll().SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                return (MovieDto)GenericMapper.MapObject(movie, new MovieDto());
            }

            return null;
        }
    }
}
