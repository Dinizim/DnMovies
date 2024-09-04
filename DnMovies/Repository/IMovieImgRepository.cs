using DnMovies.Models;
using Refit;

namespace DnMovies.Repository;

public interface IMovieImgRepository
{
    [Get("/movie/{size}/{file_path}")]
    Task<MovieImagesResponse> GetImageFromMovieAsync(string size, string file_path);
}