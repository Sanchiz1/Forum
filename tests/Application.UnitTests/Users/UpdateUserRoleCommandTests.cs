using Application.Common.Constants;
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
    public class UpdateUserRoleCommandTests
    {
        private static readonly UpdateUserRoleCommand Command = new UpdateUserRoleCommand()
        {
            User_Id = 1,
            Account_Role = Role.Admin,
            Role_Id = 1
        };

        private readonly UpdateUserRoleCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;

        public UpdateUserRoleCommandTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();

            _handler = new UpdateUserRoleCommandHandler(_userRepositoryMock);
        }

        [Fact]
        public async Task Handle_Update_User_Role_Success()
        {
            var result = await _handler.Handle(Command, default);

            var res = result.IfFail("");
            Assert.Equal("Role has been updated succesfully", res);
        }

        [Fact]
        public async Task Handle_Update_User_Role_Fail_Permission_Denied()
        {
            UpdateUserRoleCommand testCommand = Command with
            {
                Account_Role = string.Empty
            };
            var result = await _handler.Handle(testCommand, default);

            var res = result.IfSuccess(new Exception());
            Assert.Equal(new Exception("Permission denied").Message, res.Message);
        }
    }
}
