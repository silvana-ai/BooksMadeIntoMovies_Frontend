using BooksMadeIntoMovies_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BooksMadeIntoMovies_Frontend.Controllers
{
    //Säger att den här controller är en api controller
    [ApiController]
    [Route("[controller]")]
    public class FilmController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;  // variabel för att jag skall använda dependency injection

        public FilmController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("details/{imdbId}")]
        public async Task<IActionResult> Details(string imdbId)
        {
            var film = new Film();
            var client = _clientFactory.CreateClient("Imdb");

            var response = await client.GetAsync($"?i={imdbId}&apikey=2333a186");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                film = JsonConvert.DeserializeObject<Film>(content);
            }
            return View(film);
        }
    }
}
