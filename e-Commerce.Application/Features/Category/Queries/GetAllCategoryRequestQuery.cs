using e_Commerce.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Queries
{
    public class GetAllCategoryRequestQuery: IRequest<List<GetAllCategoryResponseQuery>>
    {
        
    }
}
