using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/genre")] // endpoint between client and controler
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly ILogger<GenreController> logger;

        public GenreController(IRepository repository, ILogger<GenreController> logger )  
                                                                 // when using ILogger we put <"class we are in"> so in this case <GenreController>
        {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]                                                // defines the action
        [HttpGet("list")]                                       // edits the path tho api/genre/list
        [HttpGet("/allgenres")]                                 // overwrittes the path to /allgenres
                                                                //[ResponseCache(Duration = 60)]                          // adding responsecache with time limit 60sec, first time we use this method we read it 
                                                                // -> from repository but then if we use it again in next 60 sec we read it from cache
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            return await repository.GetAllGenres();
        }

        [HttpGet("{id:int}", Name ="getGenre")]                 // alters route to api/genre/{id} so we dont get the error. {id} is a placeholder for a value api/genre/2
                                                                // id:int sets the type of value
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult<Genre> Get(int id, string param2)   // need to use ActionResult so we can return NotFound, prefer it over IActionResult
                                                                // ActionResult<returntype> - IActionResult return Ok(genre)
                                                                // [FromHeader] we will insert the string param2 in the header of the request (POST & PUT)
        {
            logger.LogDebug("get by Id executing");
            var genre = repository.GetGenreById(id);
            if (genre == null)
            {
                logger.LogWarning($"Genre with Id {id} not found");
                logger.LogError("This is an error");
                //throw new ApplicationException();

                return NotFound();
            }
            return genre;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)       //we are inserting the new genre in the body of the request
        {
            repository.AddGenre(genre);
            return new CreatedAtRouteResult("getGenre", new { Id = genre.Id }, genre);
        }
        [HttpPut]
        public ActionResult Put([FromBody] Genre genre)
        {
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
