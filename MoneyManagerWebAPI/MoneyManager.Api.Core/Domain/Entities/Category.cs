﻿using MoneyManager.Api.Core.Domain.Common;
using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class Category : BaseEntity, ICategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
