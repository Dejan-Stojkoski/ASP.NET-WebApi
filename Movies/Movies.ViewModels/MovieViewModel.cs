using Movies.ViewModels.Enums;

namespace Movies.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Genre{ private get; set; }
        public int Year { get; set; }
        public MovieGenre MovieGenre => (MovieGenre) Genre;
    }
}
