namespace Movies.Services.ModelsDto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Genre { get; set; }
        public int Year { get; set; }
    }
}
