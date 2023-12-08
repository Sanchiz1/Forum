﻿using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Application.UseCases.Posts.Commands
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _context;

        public UpdatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken) => await _context.UpdatePostAsync(request);
    }
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(5)
                .NotEmpty();
        }
    }
}