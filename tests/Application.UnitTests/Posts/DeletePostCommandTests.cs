using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using Application.UseCases.Users.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Posts
{
    public class DeletePostCommandTests
    {
        private static readonly DeletePostCommand Command = new DeletePostCommand()
        {
            Id = 1,
            Account_Id = 1,
            Account_Role = "testTitle",
        };

        private readonly DeletePostCommandHandler _handler;
        private readonly IPostRepository _postRepositoryMock;

        public DeletePostCommandTests()
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

            _handler = new DeletePostCommandHandler(_postRepositoryMock);
        }

        [Fact]
        public async Task Handle_Delete_Post_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Post has been created successfully", res);
        }

        [Fact]
        public async Task Handle_Delete_Post_Fail_Permission_Denied()
        {
            DeletePostCommand testCommand = Command with
            {
                Account_Id = 0
            };
            var result = await _handler.Handle(testCommand, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Permission denied").Message, res.Message);
        }
    }
}
