using e_Commerce.Application.Dtos;
using e_Commerce.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Queries
{
    public class GetAllProductQueryRequest: IRequest<List<GetAllProductQueryResponse>>
    {
        
    }
}
