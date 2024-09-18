using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.Business.DTOs.CommentDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;

namespace PB201MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {
            await _commentService.CreateAsync(dto);

            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _commentService.GetByExpression(true, null, "AppUser", "Movie"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CommentGetDto commentGetDto = null;
            try
            {
                commentGetDto = await _commentService.GetSingleByExpression(true, x => x.Id == id, "AppUser", "Movie");
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

            return Ok(commentGetDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
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
