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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("allUsers")]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users.Select(x => (UserViewModel)GenericMapper.MapObject(x, new UserViewModel())));
        }

        [HttpPost("addUser")]
        public ActionResult AddUser(UserAddViewModel user)
        {
            if (_userService.AddUser((UserAddDto)GenericMapper.MapObject(user, new UserAddDto())))
            {
                return Ok($"User added successfully!");
            }

            return BadRequest($"Invalid format or content missing!");
        }

        [HttpGet("user/{id:int}")]
        public ActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if(user != null)
            {
                return Ok((UserViewModel)GenericMapper.MapObject(user, new UserViewModel()));
            }

            return BadRequest($"No user with ID:{id}");
        }

        [HttpGet("rentedMovies/user/{userId:int}")]
        public ActionResult GetAllRentedMovies(int userId)
        {
            var movies = _userService.GetRentedMovies(userId);
            if(movies == null)
            {
                return BadRequest($"No user with Id : {userId}!");
            }

            if(movies.Count != 0)
            {
                return Ok(movies.Select(x => (MovieViewModel)GenericMapper.MapObject(x, new MovieViewModel())));
            }

            return NoContent();
        }

        [HttpGet("rentedMovies/genre")]
        public ActionResult GetAllRentedMoviesByGenre([FromQuery]int? userId, MovieGenre? genre)
        {
            if(userId == null || genre == null)
            {
                return BadRequest("Query parameters missing!");
            }

            var movies = _userService.GetRentedMoviesByGenre(userId, (int)genre);
            if(movies == null)
            {
                return BadRequest($"No user with Id : {userId}!");
            }

            if (movies.Count != 0)
            {
                return Ok(movies.Select(x => (MovieViewModel)GenericMapper.MapObject(x, new MovieViewModel())));
            }

            return NoContent();
        }
    }
}
