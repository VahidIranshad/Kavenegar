using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Features.BlogFeatures.Command.Create;
using Kavenegar.Application.Features.BlogFeatures.Command.Delete;
using Kavenegar.Application.Features.BlogFeatures.Command.Update;
using Kavenegar.Application.Features.BlogFeatures.Query.GetByID;
using Kavenegar.Application.Features.BlogFeatures.Query.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Kavenegar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var blogList = await _mediator.Send(new BlogGetListQuery());
            return Ok(blogList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _mediator.Send(new GetBlogByIDQuery { Id = id });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlogCrudDto blogCrudDto)
        {
            var command = new BlogCreateCommand { blogCrudDto = blogCrudDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BlogCrudDto blogCrudDto)
        {
            var command = new BlogUpdateCommand { blogCrudDto = blogCrudDto };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new BlogDeleteCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
