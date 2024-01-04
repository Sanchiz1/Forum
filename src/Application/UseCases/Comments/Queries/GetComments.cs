using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class GetCommentsQuery : IRequest<Result<List<CommentViewModel>>>
    {
        public int Post_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Order { get; set; } = "Date";
        public int User_Id { get; set; } = 0;
    }
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Result<List<CommentViewModel>>>
    {
        private readonly ICommentRepository _context;

        public GetCommentsQueryHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<CommentViewModel>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken) => await _context.GetCommentsAsync(request);
    }
}
