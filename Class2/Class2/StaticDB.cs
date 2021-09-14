using System.Collections.Generic;
using Class2.Models;

namespace Class2
{
    public static class StaticDB
    {
        public static int UserId = 3;
        public static List<User> Users = new List<User>()
        {
            new User
            {
                Id = 1,
                FirstName = "Bill",
                LastName = "Billsky",
                Age = 17
            },
            new User
            {
                Id = 2,
                FirstName = "Greg",
                LastName = "Gregsky",
                Age = 27
            },
            new User
            {
                Id = 3,
                FirstName = "Jill",
                LastName = "Jillsky",
                Age = 32
            }
        };
    }
}
