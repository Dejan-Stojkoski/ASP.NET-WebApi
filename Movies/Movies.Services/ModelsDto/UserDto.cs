namespace Movies.Services.ModelsDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int? FavouriteGenre { get; set; }
        public string Token { get; set; }
    }
}
