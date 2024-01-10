using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Users.Commands;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Users
{
    public class UpdateUserCommandTests
    {
        private static readonly UpdateUserCommand Command = new UpdateUserCommand()
        {
            Account_Id = 1,
            Username = "testUsername",
            Email = "testemail@gmail.com",
            Bio = "testBio",
        };

        private readonly UpdateUserCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;

        public UpdateUserCommandTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();

            _handler = new UpdateUserCommandHandler(_userRepositoryMock);
        }

        [Fact]
        public async Task Handle_Update_Account_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Account has been updated successfully", res);
        }

        [Fact]
        public async Task Handle_Update_Account_Fail_Username_Already_Exists()
        {
            _userRepositoryMock.GetUserByUsernameAsync(Command.Username).Returns(new UserViewModel()
            {
                User = new User()
                {
                    Id = 2,
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
        public async Task Handle_Update_Account_Fail_Email_Already_Exists()
        {
            _userRepositoryMock.GetUserByEmailAsync(Command.Email).Returns(new UserViewModel()
            {
                User = new User()
                {
                    Id = 2,
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
