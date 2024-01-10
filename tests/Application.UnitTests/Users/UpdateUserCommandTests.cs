using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Commands;
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
    }
}
