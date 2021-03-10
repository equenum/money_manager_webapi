using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
