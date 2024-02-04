﻿using AutoMapper;
using e_Commerce.Application.Features.Product.Commands;
using e_Commerce.Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById")]
        public async Task<GetByIdProductQueriesResponse> GetById([FromQuery] GetByIdProductQueriesRequest request)
        {
            var resp = await _mediator.Send(request);
            return resp;
        }

        [HttpGet("GetAllProduct")]
        public async Task<List<GetAllProductQueryResponse>> GetAllProduct([FromQuery]GetAllProductQueryRequest request)
        {  
            var resp= await _mediator.Send(request);
            return resp;

        }

        [HttpPost("Add")]
        public async Task<AddProductCommandResponse> Add(AddProductCommandRequest request)
        {
            var resp= await _mediator.Send(request);
            return resp;
        }

        [HttpPut("Update")]
        public async Task<UpdateProductCommandResponse> Update(UpdateProductCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("Delete")]
        public async Task<DeleteProductCommandResponse> Delete(DeleteProductCommandRequest request)
        {
            return await _mediator.Send(request);
        }
        
    }
}