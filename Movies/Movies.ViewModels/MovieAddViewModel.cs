namespace Movies.ViewModels
{
    public class MovieAddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Genre { private get; set; }
        public int Year { get; set; }
    }
}
