namespace DnMovies.Models
{
    public class MoviesViewModel
    {
        public MovieResponse? TopRatedMovies { get; set; }
        public MovieResponse? PopularMovies { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
