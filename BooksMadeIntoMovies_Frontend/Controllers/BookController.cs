using BooksMadeIntoMovies_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooksMadeIntoMovies_Frontend.Controllers
{
    //Säger att den här controller är en api controller
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IHttpClientFactory _clientFactory; //För att använda _clientFactory behöver jag injecta den genom konstruktör. _clientFactory använder jag för att göra http request.
        private readonly IMemoryCache _cache;

        public BookController(IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _cache = cache;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListOfBooks()
        {
            if (!_cache.TryGetValue("AllBooks", out List<Book> books)) //Om caching finns, returnera books annars går jag till databasen och hämtar books.
            {
                var client = _clientFactory.CreateClient("backendAPI");

                var response = await client.GetAsync("api/book/all");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<List<Book>>(content);
                    _cache.Set("AllBooks", books);
                }
            }
            return View(books);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> DetailsOfBook(int id)
        {
            var book = new Book();
            var client = _clientFactory.CreateClient("backendAPI");

            var response = await client.GetAsync($"api/book/details/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<Book>(content);
            }
            return View(book);
        }
    }
}
