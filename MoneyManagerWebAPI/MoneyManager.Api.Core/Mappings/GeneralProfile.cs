using AutoMapper;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Dtos.User;
using MoneyManager.Api.Core.Features.Categories.Commands;
using MoneyManager.Api.Core.Features.Users.Commands;
using MoneyManager.Api.Core.Features.Users.Queries;
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
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserAuthDto>().ReverseMap();
            CreateMap<AuthenticateUser.Query, User>();
            CreateMap<RegisterUser.Command, User>();
        }
    }
}
