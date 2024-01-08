using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Users.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Posts
{
    public class CreatePostCommandTests
    {
        private static readonly CreatePostCommand Command = new CreatePostCommand()
        {
            Account_Id = 1,
            Title = "testTitle",
            Text = "testText",
        };

        private readonly CreatePostCommandHandler _handler;
        private readonly IPostRepository _postRepositoryMock;

        public CreatePostCommandTests()
        {
            _postRepositoryMock = Substitute.For<IPostRepository>();

            _handler = new CreatePostCommandHandler(_postRepositoryMock);
        }

        [Fact]
        public async Task Handle_Create_Post_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Post has been created successfully", res);
        }

        [Fact]
        public async Task Handle_Create_Post_Fail_Permission_Denied()
        {
            CreatePostCommand testCommand = Command with
            {
                Account_Id = 0
            };
            var result = await _handler.Handle(testCommand, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Permission denied").Message, res.Message);
        }
    }
}
