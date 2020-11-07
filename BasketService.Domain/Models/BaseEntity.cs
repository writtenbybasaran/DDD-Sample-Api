using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
