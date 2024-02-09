using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Application.Features.Category.Queries;
using e_Commerce.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById")]
        public async Task<DataResult> GetById([FromQuery]GetByIdCategoryRequestQuery request)
        {
            var resp = await _mediator.Send(request);
            return resp;
        }

        [HttpGet("GetAllCategory")]
        public async Task<DataResult> GetAllCategory([FromQuery] GetAllCategoryRequestQuery request)
        {
            var resp = await _mediator.Send(request);
            return resp;

        }

        [HttpPost("Add")]
        public async Task<DataResult> Add([FromBody] AddCategoryCommandRequest request)
        {
            var resp = await _mediator.Send(request);
            return resp;
        }

        [HttpPut("Update")]
        public async Task<DataResult> Update([FromBody] UpdateCategoryCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("Delete")]
        public async Task<DataResult> Delete(DeleteCategoryCommandRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
