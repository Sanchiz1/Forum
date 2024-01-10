using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Queries;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Users
{
    public class CreateUserCommandTests
    {
        private static readonly CreateUserCommand Command = new CreateUserCommand()
        {
            Username = "testUsername",
            Email = "testemail@gmail.com",
            Bio = "testBio",
            Password = "testPassword"
        };

        private readonly CreateUserCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IHashingService _hashingServiceMock;

        public CreateUserCommandTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _hashingServiceMock = Substitute.For<IHashingService>();

            _handler = new CreateUserCommandHandler(_userRepositoryMock, _hashingServiceMock);
        }

        [Fact]
        public async Task Handle_Create_Account_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Account has been created successfully", res);
        }

        [Fact]
        public async Task Handle_Create_Account_Fail_Username_Already_Exists()
        {
            _userRepositoryMock.GetUserByUsernameAsync(Command.Username).Returns(new UserViewModel()
            {
                User = new User()
                {
                    Id = 1,
                    Username = "testUsername",
                    Email = "testemail@gmail.com",
                    Registered_At = DateTime.Now
                },
                Role = string.Empty
            });

            var result = await _handler.Handle(Command, default);

            var res = result.IfSuccess(new Exception(""));
            Assert.Equal("User with this username already exists", res.Message);
        }

        [Fact]
        public async Task Handle_Create_Account_Fail_Email_Already_Exists()
        {
            _userRepositoryMock.GetUserByEmailAsync(Command.Email).Returns(new UserViewModel()
            {
                User = new User()
                {
                    Id = 1,
                    Username = "testUsername",
                    Email = "testemail@gmail.com",
                    Registered_At = DateTime.Now
                },
                Role = string.Empty
            });

            var result = await _handler.Handle(Command, default);

            var res = result.IfSuccess(new Exception(""));
            Assert.Equal("User with this email already exists", res.Message);
        }
    }
}
