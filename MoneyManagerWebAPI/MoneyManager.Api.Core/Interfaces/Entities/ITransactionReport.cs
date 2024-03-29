﻿using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MoneyManager.Api.Core.Interfaces.Entities
{
    public interface ITransactionReport
    {
        int Total { get; set; }
        List<Transaction> Transactions { get; set; }
    }
}