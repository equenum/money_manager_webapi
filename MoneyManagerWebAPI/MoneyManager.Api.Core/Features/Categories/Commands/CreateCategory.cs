using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Interfaces;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Commands
{
    public static class CreateCategory
    {
        public class Command : IRequest<Response>
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            //private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO: Validate if already exists
                var category = _mapper.Map<Category>(request);
                var existingCategory = _unitOfWork.Categories.Find(c => c.Name == category.Name);

                var response = new Response();

                if (existingCategory != null)
                {
                    response.ErrorMessage = "Category already exists!";
                    response.StatusCode = 409;
                }

                category.Created = DateTime.Now;
                category.Modified = DateTime.Now;
                _unitOfWork.Categories.Add(category);

                var createdCategory = _unitOfWork.Categories.Find(c => c.Name == category.Name);
                response.Content = _mapper.Map<CategoryDto>(createdCategory);

                return response;
            }
        }

        public class Response
        {
            public CategoryDto Content { get; set; }
            public int StatusCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
