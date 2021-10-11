namespace Movies.Services.ModelsDto
{
    public class RegisterUserDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? FavouriteGenre { get; set; }
    }
}
