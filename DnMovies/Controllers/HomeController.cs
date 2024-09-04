using DnMovies.Models;
using DnMovies.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DnMovies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieImgRepository _movieImgRepository;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, IMovieImgRepository movieImgRepository, IConfiguration configuration)
    {
        _logger = logger;
        _movieRepository = movieRepository;
        _movieImgRepository = movieImgRepository;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var movieResponse = await _movieRepository.GetMoviesAsync("pt-BR", 1);
        var BaseImgURl = _configuration["TMDB:BaseImageUrl"];
        foreach (var movie in movieResponse.Results)
        {
            movie.Backdrop_path = $"{BaseImgURl}{movie.Backdrop_path}";
            movie.Poster_path = $"{BaseImgURl}{movie.Poster_path}";
        }

        _logger.LogInformation("Resposta recebida da API: {Response}", JsonConvert.SerializeObject(movieResponse));
        return View(movieResponse);
    }
}