
^****BooksMadeInToMoves-Fronted****

På fronted en client ska ha en sida som inhåller tre Controller:

*Home page with https://localhost:44325/

*Books page with https://localhost:44325/Book/list

I booksController jag här användat _clientFactory och För att använda _clientFactory behöver jag injecta den genom konstruktör. _clientFactory använder jag för att göra http request.

Jag har användat också i aysenc metod [HttpGet("list")] of books en memory cache,Om caching finns, returnera books annars går jag till databasen och hämtar books.

Jag har användat också i aysenc metod   [HttpGet("details/{id}")] för att hämta details of book med Id.


*Details of en book with Id :https://localhost:44325/Book/Details/1

*Open API details för en Film på den booken med Id :https://localhost:44325/Film/Details/tt0031381

I den här projekt vi har tre Models och varje model ska inhåler deras Classer:

*Books

*ErrorViewModel

*Film

programet körs i Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.