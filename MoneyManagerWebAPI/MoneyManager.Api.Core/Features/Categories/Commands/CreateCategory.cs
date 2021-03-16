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
            [MaxLength(255)]
            public string Name { get; set; }

            [Required]
            [MaxLength(2000)]
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request);
                var existingCategory = await _unitOfWork.Categories.FindAsync(c => c.Name == category.Name);

                var response = new Response();

                if (existingCategory != null)
                {
                    response.Message = "Category already exists!";
                    response.StatusCode = 409;

                    return response;
                }

                category.Created = DateTime.Now;
                category.Modified = DateTime.Now;
                
                var createdCategory = await _unitOfWork.Categories.AddAsync(category);
                response.Content = _mapper.Map<CategoryDto>(createdCategory);

                return response;
            }
        }

        public class Response
        {
            public CategoryDto Content { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
