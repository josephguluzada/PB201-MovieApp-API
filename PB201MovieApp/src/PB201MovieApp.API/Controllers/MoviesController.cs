using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _movieService.GetByExpression(true,null,"Genre"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto dto)
        {
            MovieGetDto movie = null;
            try
            {
                movie = await _movieService.CreateAsync(dto);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Created(new Uri($"api/movies/{movie.Id}", UriKind.Relative),movie);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MovieGetDto dto = null;
            try
            {
                dto = await _movieService.GetById(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] MovieUpdateDto dto)
        {
            try
            {
                await _movieService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
