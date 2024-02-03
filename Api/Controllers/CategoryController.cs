using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Application.Features.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById")]
        public async Task<GetByIdCategoryResponseQuery> GetById([FromQuery]GetByIdCategoryRequestQuery request)
        {
            var resp = await _mediator.Send(request);
            return resp;
        }

        [HttpGet("GetAllCategory")]
        public async Task<List<GetAllCategoryResponseQuery>> GetAllCategory([FromQuery] GetAllCategoryRequestQuery request)
        {
            var resp = await _mediator.Send(request);
            return resp;

        }

        [HttpPost("Add")]
        public async Task<AddCategoryCommandResponse> Add([FromBody] AddCategoryCommandRequest request)
        {
            var resp = await _mediator.Send(request);
            return resp;
        }

        [HttpPut("Update")]
        public async Task<UpdateCategoryCommandResponse> Update([FromBody] UpdateCategoryCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("Delete")]
        public async Task<DeleteCategoryCommandResponse> Delete(DeleteCategoryCommandRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
