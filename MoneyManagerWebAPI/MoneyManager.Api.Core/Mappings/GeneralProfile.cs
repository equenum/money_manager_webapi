using AutoMapper;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
