using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        public IActionResult GetMovies()
        {
            return Ok(MoviesDataStore.Current.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            var movie = MoviesDataStore.Current
            .Movies.FirstOrDefault(x => x.Id == id);

            if(movie == null){
                return NotFound();
            }            
            return Ok(movie);
        }
    }
}