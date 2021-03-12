using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Features.Categories.Queries
{
    public class GetAllCategoriesQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCategoriesQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public GetAllCategoriesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
