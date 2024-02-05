using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Response
{
    public class DataResult
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public object Message { get; set; }
    }
}
