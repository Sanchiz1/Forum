using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.UseCases.Comments.Commands;
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
    public class UpdateCategoryCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Account_Role { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
    {
        private readonly ICategoryRepository _context;

        public UpdateCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role != Role.Admin)
            {
                return new Exception("Permission denied");
            }
            await _context.UpdateCategoryAsync(request);

            return "Category updted successfully";
        }
    }
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Title)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
