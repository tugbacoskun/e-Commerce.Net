using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Dtos
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
