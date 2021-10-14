using System;
using System.Linq;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/casts")]
    public class CastController : ControllerBase
    {
        private ILogger<CastController> _logger;
        private IMailService _localMailService;

        public CastController(ILogger<CastController> logger, IMailService localMailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetCasts(int movieId)
        {
            var movie = MoviesDataStore.Current.
            Movies.FirstOrDefault(x => x.Id == movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie.Casts);
        }

        [HttpGet("{id}", Name = "GetCast")]
        public IActionResult GetCast(int movieId, int id)
        {
            try
            {
                var movie = MoviesDataStore.Current
                .Movies.FirstOrDefault(x => x.Id == movieId);

                if (movie == null)
                {
                    return NotFound();
                }

                var cast = movie.Casts.FirstOrDefault(x => x.Id == id);

                if (cast == null)
                {
                    _logger.LogInformation($"El cast con id {id} no fue encontrado");
                    return NotFound();
                }

                return Ok(cast);
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Un error ocurrio al buscar el cast con id {id}", ex);
                return StatusCode(500, "Un problema ocurrio al realizar la solicitud al recurso");
            }
        }


        [HttpPost]

        public IActionResult createCast(int movieId, [FromBody] CastForCreationDto castForCreationDto)
        {
            var movie = MoviesDataStore.Current.Movies.FirstOrDefault(x => x.Id == movieId);

            if (movie == null)
            {
                return NotFound();
            }

            var maxCastId = MoviesDataStore.Current.Movies.SelectMany(x => x.Casts).Max(p => p.Id);

            var newCast = new CastDto
            {
                Id = ++maxCastId,
                Name = castForCreationDto.Name,
                Character = castForCreationDto.Character
            };

            movie.Casts.Add(newCast);

            return CreatedAtRoute(
                nameof(GetCast),
                new { movieId, id = newCast.Id },
                castForCreationDto
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCast(int movieId, int id, [FromBody] CastForCreationDto castForUpdate)
        {
            var movie = MoviesDataStore.Current.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
            {
                return NotFound();
            }

            var castFromStore = movie.Casts.FirstOrDefault(x => x.Id == id);
            if (castFromStore == null)
            {
                return NotFound();
            }

            castFromStore.Name = castForUpdate.Name;
            castFromStore.Character = castForUpdate.Character;

            return NoContent(); // en estos casos devolver un 204 el recurso ha sido actualizado
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateCast(int movieId, int id, [FromBody] JsonPatchDocument<CastForUpdateDto> patchDocument)
        {
            var movie = MoviesDataStore.Current.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
            {
                return NotFound();
            }

            var castFromStore = movie.Casts.FirstOrDefault(x => x.Id == id);
            if (castFromStore == null)
            {
                return NotFound();
            }

            var castToPatch = new CastForUpdateDto()
            {
                Name = castFromStore.Name,
                Character = castFromStore.Character
            };

            patchDocument.ApplyTo(castToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(castToPatch))
            {
                return BadRequest(ModelState);
            }

            castFromStore.Name = castToPatch.Name;
            castFromStore.Character = castToPatch.Character;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCast(int movieId, int id)
        {
            var movie = MoviesDataStore.Current.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
            {
                return NotFound();
            }

            var castFromStore = movie.Casts.FirstOrDefault(x => x.Id == id);
            if (castFromStore == null)
            {
                return NotFound();
            }

            _localMailService.Send("Recurso Eliminado", $"El recurso con id {id} fue eliminado");

            movie.Casts.Remove(castFromStore);

            return NoContent();
        }

    }
}