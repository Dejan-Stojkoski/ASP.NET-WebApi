using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] LoginViewModel login)
        {
            try
            {
                var user = _userService.Authenticate(login.Username, login.Password);
                if(user != null)
                {
                    return Ok((AuthenticatedUserViewModel)GenericMapper.MapObject(user, new AuthenticatedUserViewModel()));
                }

                return NotFound("User does not exist!");
            }
            catch
            {
                return BadRequest("Username or password is incorrect!");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult RegisterUser(RegisterUserViewModel user)
        {
            if (_userService.Register((RegisterUserDto)GenericMapper.MapObject(user, new RegisterUserDto())))
            {
                return Ok($"User added successfully!");
            }

            return BadRequest($"Invalid format or content missing!");
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

        [HttpDelete("deleteUser/{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            if (_userService.DeleteUser(id))
            {
                return Ok($"User with ID: {id} deleted!");
            }

            return NotFound();
        }
    }
}
