using DnMovies.Models;
using Refit;

namespace DnMovies.Repository;

public interface IMovieRepository
{
    [Get("/movie/popular")]
    Task<MovieResponse> GetMoviesAsync([Query] string language, [Query] int page);
}