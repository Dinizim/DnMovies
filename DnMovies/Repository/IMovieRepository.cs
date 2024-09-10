using DnMovies.Models;
using Refit;

namespace DnMovies.Repository;

public interface IMovieRepository
{
    [Get("/movie/popular")]
    Task<MovieResponse> GetPopularMoviesAsync([Query] string language, [Query] int page);
    [Get("/movie/top_rated")]
    Task<MovieResponse> GetTopRatedMoviesAsync([Query] string language, [Query] int page);
    [Get("/search/movie")]
    Task<MovieResponse> MovieSearch([Query] string query, [Query] bool include_adult, [Query] string language, [Query] int page);


   
}