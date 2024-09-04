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

    public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    public async Task<IActionResult> Index()
    {
        var movieResponse = await _movieRepository.GetMoviesAsync("pt-BR", 1);
        if (movieResponse != null)
        {
            _logger.LogInformation("Resposta recebida da API: {Response}", JsonConvert.SerializeObject(movieResponse));
        }
        return View(movieResponse);
    }
}