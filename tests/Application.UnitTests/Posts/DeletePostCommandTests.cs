using Application.Common.Constants;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Posts
{
    public class DeletePostCommandTests
    {
        private static readonly DeletePostCommand Command = new DeletePostCommand()
        {
            Id = 1,
            Account_Id = 1,
            Account_Role = string.Empty,
        };

        private readonly DeletePostCommandHandler _handler;
        private readonly IPostRepository _postRepositoryMock;

        public DeletePostCommandTests()
        {
            _postRepositoryMock = Substitute.For<IPostRepository>();

            _handler = new DeletePostCommandHandler(_postRepositoryMock);
        }

        [Fact]
        public async Task Handle_Delete_Post_Success()
        {
            _postRepositoryMock.GetPostByIdAsync(Command.Id).Returns(new PostViewModel()
            {
                Post = new Post()
                {
                    Id = 1,
                    Title = "testTitle",
                    Text = "testText",
                    User_Id = 1
                },
                Liked = false,
                Likes = 0,
                Comments = 0
            });
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Post has been created successfully", res);
        }

        [Fact]
        public async Task Handle_Delete_Post_Fail_Permission_Denied()
        {
            _postRepositoryMock.GetPostByIdAsync(Command.Id).Returns(new PostViewModel()
            {
                Post = new Post()
                {
                    Id = 1,
                    Title = "testTitle",
                    Text = "testText",
                    User_Id = 2
                },
                Liked = false,
                Likes = 0,
                Comments = 0
            });
            var result = await _handler.Handle(Command, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Permission denied").Message, res.Message);
        }
    }
}
