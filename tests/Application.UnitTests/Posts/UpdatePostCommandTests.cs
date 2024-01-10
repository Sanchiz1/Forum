using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using NSubstitute;

namespace Application.UnitTests.Posts
{
    public class UpdatePostCommandTests
    {
        private static readonly UpdatePostCommand Command = new UpdatePostCommand()
        {
            Id = 1,
            Text = "testText",
            Account_Id = 1,
        };

        private readonly UpdatePostCommandHandler _handler;
        private readonly IPostRepository _postRepositoryMock;

        public UpdatePostCommandTests()
        {
            _postRepositoryMock = Substitute.For<IPostRepository>();
            _postRepositoryMock.GetPostByIdAsync(new GetPostByIdQuery() { Id = 1 }).Returns(Task.FromResult(new PostViewModel()
            {
                Post = new PostDto()
                {
                    Id = 1,
                    Title = "testTitle",
                    Text = "testText",
                    User_Id = 1
                },
                Liked = false,
                Likes = 0,
                Comments = 0
            }));

            _handler = new UpdatePostCommandHandler(_postRepositoryMock);
        }

        [Fact]
        public async Task Handle_Update_Post_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Post has been updated successfully", res);
        }
    }
}
