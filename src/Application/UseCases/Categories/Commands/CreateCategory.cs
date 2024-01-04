using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Replies.Commands;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Result<string>>
    {
        public string Title { get; set; }
        public string Account_Role { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<string>>
    {
        private readonly ICategoryRepository _context;

        public CreateCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) 
        {
            if(request.Account_Role != Role.Admin)
            {
                return new Exception("Permission denied");
            }
            await _context.CreateCategoryAsync(request);

            return "Category created succesfully";
        }
    }
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Title)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
