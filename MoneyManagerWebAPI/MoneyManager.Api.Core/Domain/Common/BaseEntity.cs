using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
