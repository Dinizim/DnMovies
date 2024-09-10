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
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, IConfiguration configuration)
    {
        _logger = logger;
        _movieRepository = movieRepository;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var topRatedMovies = await _movieRepository.GetTopRatedMoviesAsync("pt-BR", 1);
        var popularMovies = await _movieRepository.GetPopularMoviesAsync("pt-BR", 1);
        var BaseImgURl = _configuration["TMDB:BaseImageUrl"];

        if (!String.IsNullOrEmpty(searchString))
        {
            topRatedMovies = await _movieRepository.MovieSearch(searchString, false, "pt-BR", 1);
        }
        
        foreach (var movie in topRatedMovies.Results)
        {
            movie.Backdrop_path = $"{BaseImgURl}{movie.Backdrop_path}";
            movie.Poster_path = $"{BaseImgURl}{movie.Poster_path}";
        }
        foreach (var movie in popularMovies.Results)
        {
            movie.Backdrop_path = $"{BaseImgURl}{movie.Backdrop_path}";
            movie.Poster_path = $"{BaseImgURl}{movie.Poster_path}";
        }

        var viewModel = new MoviesViewModel
        {
            TopRatedMovies = topRatedMovies,
            PopularMovies = popularMovies
        };

        ViewBag.CurrentFilter = searchString;

        _logger.LogInformation("Resposta recebida da API: {Response}", JsonConvert.SerializeObject(viewModel));

        return View(viewModel);
    }
}