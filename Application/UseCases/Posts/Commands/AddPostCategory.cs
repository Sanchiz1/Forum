using Application.Common.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Commands
{
    public class AddPostCategoryCommand : IRequest
    {
        public int Post_Id {  get; set; }
        public int Category_Id {  get; set; }
    }
    public class AddPostCategoryCommandHandler : IRequestHandler<AddPostCategoryCommand>
    {
        private readonly IPostRepository _context;

        public AddPostCategoryCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(AddPostCategoryCommand request, CancellationToken cancellationToken) => await _context.AddPostCategoryAsync(request);
    }
    public class AddPostCategoryCommandValidator : AbstractValidator<AddPostCategoryCommand>
    {
        public AddPostCategoryCommandValidator()
        {
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.Category_Id)
                .NotEmpty();
        }
    }
}
