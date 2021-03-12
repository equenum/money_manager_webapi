using AutoMapper;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Features.Categories.Commands;
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
            CreateMap<CreateCategory.Command, Category>();
        }
    }
}
