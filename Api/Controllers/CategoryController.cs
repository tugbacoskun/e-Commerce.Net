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
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            try
            {
                _logger.LogInformation("Request Name : GetAllCategory");
                var resp = await _mediator.Send(request);
                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                throw;
            } 
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
