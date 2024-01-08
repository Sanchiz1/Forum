using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.UseCases.Users.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
