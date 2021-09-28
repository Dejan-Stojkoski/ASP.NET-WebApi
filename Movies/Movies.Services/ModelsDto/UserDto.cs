namespace Movies.Services.ModelsDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int? Subscription { get; set; }
    }
}
