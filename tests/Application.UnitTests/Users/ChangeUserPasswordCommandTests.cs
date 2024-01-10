using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.UseCases.Posts.Queries;
using Application.UseCases.Users.Commands;
using NSubstitute;

namespace Application.UnitTests.Users
{
    public class ChangeUserPasswordCommandTests
    {
        private static readonly ChangeUserPasswordCommand Command = new ChangeUserPasswordCommand()
        {
            Account_Id = 1,
            Password = "testPassword",
            New_Password = "testNewPassword"
        };

        private readonly ChangeUserPasswordCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IHashingService _hashingServiceMock;

        public ChangeUserPasswordCommandTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _hashingServiceMock = Substitute.For<IHashingService>();


           _handler = new ChangeUserPasswordCommandHandler(_userRepositoryMock, _hashingServiceMock);
        }

        [Fact]
        public async Task Handle_Change_User_Password_Success()
        {
            _userRepositoryMock.GetUserSaltAsync(Command.Account_Id).Returns("");
            _userRepositoryMock.GetUserPasswordAsync(Command.Account_Id).Returns("password");
            _hashingServiceMock.ComputeHash(Command.Password, "").Returns("password");
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Password has been changed successfully", res);
        }

        [Fact]
        public async Task Handle_Change_User_Password_Fail_Wrong_Password()
        {
            _userRepositoryMock.GetUserSaltAsync(Command.Account_Id).Returns("");
            _userRepositoryMock.GetUserPasswordAsync(Command.Account_Id).Returns("password");
            _hashingServiceMock.ComputeHash(Command.Password, "").Returns("password2");

            var result = await _handler.Handle(Command, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Wrong password").Message, res.Message);
        }
    }
}
