using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.Business.DTOs.GenreDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;

namespace PB201MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genreService.GetByExpression());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GenreGetDto? dto = null;
            try
            {
                dto = await _genreService.GetById(id);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            try
            {
                await _genreService.CreateAsync(dto);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpdateDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);
            }
            catch(InvalidIdException)
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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);
            }
            catch(InvalidIdException)
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
