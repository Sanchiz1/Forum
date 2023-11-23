using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserViewModel> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery);
        Task<UserViewModel> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByUsernameQuery);
        Task<UserViewModel> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery);
        Task<UserViewModel> GetUserByCredentialsAsync(GetUserByCredentialsQuery getUserByCredentialsQuery);
        Task CreateUserAsync(CreateUserCommand createUserCommand);
        Task UpdateUserAsync(UpdateUserCommand updateUserCommand);
        Task DeleteUserAsync(DeleteUserCommand deleteUserCommand);
        Task AddUserRoleAsync(AddUserRoleCommand addUserRoleCommand);
        Task RemoveUserRoleAsync(RemoveUserRoleCommand removeUserRoleCommand);
    }
}
