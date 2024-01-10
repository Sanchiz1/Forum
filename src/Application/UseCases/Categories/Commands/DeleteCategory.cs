using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Account_Role { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<string>>
    {
        private readonly ICategoryRepository _context;

        public DeleteCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) 
        {
            if (request.Account_Role != Role.Admin)
            {
                return new Exception("Permission denied");
            }
            await _context.DeleteCategoryAsync(request);

            return "Category deleted successfully";
        }
    }
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
