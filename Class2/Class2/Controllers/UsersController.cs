using System.Collections.Generic;
using System.Linq;
using Class2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Class2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("getAllUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            return StaticDB.Users;
        }

        [HttpGet("getUser/{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
            var user = StaticDB.Users.FirstOrDefault(x => x.Id == userId);
            if(user != null)
            {
                return user;
            }

            return NotFound($"User with ID:{userId} does not exist!");
        }

        [HttpGet("adultCheck/userId/{userId}")]
        public ActionResult CheckIfUserIsAdult(int userId)
        {
            var user = StaticDB.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var message = "";
                message = user.Age > 17 ? $"The user with ID:{userId} is an adult!" : $"The user with ID:{userId} is not an adult!";

                return Ok(message);
            }

            return NotFound($"User with ID:{userId} does not exist!");
        }
    }
}
