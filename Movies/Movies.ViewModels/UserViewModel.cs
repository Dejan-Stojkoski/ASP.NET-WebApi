using Movies.ViewModels.Enums;

namespace Movies.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int? FavouriteGenre { private get; set; }
        public MovieGenre FavouriteGenreType => (MovieGenre)FavouriteGenre;
    }
}
