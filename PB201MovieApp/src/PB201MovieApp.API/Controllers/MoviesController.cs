using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.API.ApiResponses;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;

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
            return Ok(new ApiResponse<ICollection<MovieGetDto>>
            {
                Data = await _movieService.GetByExpression(true, null, "Genre", "MovieImages", "Comments.AppUser"),
                ErrorMessage = null,
                PropertyName = null,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto dto)
        {
            MovieGetDto movie = null;
            try
            {
                movie = await _movieService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Created();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MovieGetDto dto = null;
            try
            {
                dto = await _movieService.GetSingleByExpression(false, x=>x.Id == id, "Genre", "MovieImages", "Comments.AppUser");
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest, //400
                    ErrorMessage = "Id yanlisdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovieGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<MovieGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK
            });
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
                return BadRequest(new ApiResponse<MovieUpdateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest, //400
                    ErrorMessage = "Id yanlisdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovieUpdateDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovieUpdateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
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
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest, //400
                    ErrorMessage = "Id yanlisdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok();
        }
    }
}
