using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
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
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Account_Role { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _context;

        public DeleteCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) 
        {
            if (request.Account_Role != "Admin")
            {
                throw new PermissionException();
            }
            await _context.DeleteCategoryAsync(request); 
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
