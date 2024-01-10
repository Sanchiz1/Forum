using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.UseCases.Users.Commands;
using NSubstitute;

namespace Application.UnitTests.Users
{
    public class DeleteUserCommandTests
    {
        private static readonly DeleteUserCommand Command = new DeleteUserCommand()
        {
            User_Id = 1,
            Account_Id = 1,
            Account_Role = string.Empty,
            Password = "testPassword"
        };

        private readonly DeleteUserCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IHashingService _hashingServiceMock;

        public DeleteUserCommandTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _hashingServiceMock = Substitute.For<IHashingService>();

            _handler = new DeleteUserCommandHandler(_userRepositoryMock, _hashingServiceMock);
        }

        [Fact]
        public async Task Handle_Delete_Account_Owner_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Account has been deleted successfully", res);
        }


        [Fact]
        public async Task Handle_Delete_Account_Admin_Success()
        {
            DeleteUserCommand testCommand = Command with
            {
                User_Id = 1,
                Account_Id = 2,
                Account_Role = Role.Admin
            };
            var result = await _handler.Handle(testCommand, default);

            var res = result.IfFail("");
            Assert.Equal("Account has been deleted successfully", res);
        }

        [Fact]
        public async Task Handle_Create_Account_Fail_Permission_Denied()
        {
            DeleteUserCommand testCommand = Command with
            {
                User_Id = 1,
                Account_Id = 2,
                Account_Role = string.Empty
            };
            var result = await _handler.Handle(testCommand, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Permission denied").Message, res.Message);
        }
    }
}
